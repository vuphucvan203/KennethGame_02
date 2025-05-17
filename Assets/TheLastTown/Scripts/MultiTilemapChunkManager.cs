using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MultiTilemapChunkManager : KennMonoBehaviour
{
    [Header("Setup")]
    public Transform gridRoot; 
    public Transform player;   
    public int chunkSize = 16; 
    public int loadRadius = 5;
    public int existRadius = 10;
    //public bool isPrewview = false;
    public TileBase[] tilePalette;

    private Dictionary<string, Tilemap> tilemapsByName = new();
    //private Dictionary<string, Dictionary<Vector2Int, TileBase[,]>> allChunkData = new();
    private Dictionary<string, Dictionary<Vector2Int, TileCell[,]>> allChunkData = new();
    private Dictionary<string, HashSet<Vector2Int>> loadedChunksPerLayer = new();
    //private Queue<(string layerName, Vector2Int coord)> chunkLoadQueue = new();
    private Queue<Vector2Int> chunkCoordQueue = new();
    private bool isLoadingChunks = false;
    private HashSet<Vector2Int> queuedChunk = new();

#if UNITY_EDITOR
    [ContextMenu("Load All Base Tile From Assets")]
    public void LoadAllTilesFromAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:TileBase");
        List<TileBase> tiles = new();
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TileBase tile = AssetDatabase.LoadAssetAtPath<TileBase>(path);
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }

        tilePalette = tiles.ToArray();
        Debug.Log($"Success to load all Tile Base.");
    }
#endif

    //[ContextMenu("Extract Chunks From Tilemaps")]
    public void ExtractChunksFromAllTilemaps()
    {
        tilemapsByName.Clear();
        allChunkData.Clear();

        var tilemaps = gridRoot.GetComponentsInChildren<Tilemap>();

        foreach (var tilemap in tilemaps)
        {
            string layerName = tilemap.gameObject.name;
            tilemapsByName[layerName] = tilemap;

            //var chunkDictionary = new Dictionary<Vector2Int, TileBase[,]>();
            var chunkDictionary = new Dictionary<Vector2Int, TileCell[,]>();
            BoundsInt bounds = tilemap.cellBounds;
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new(x, y, 0);
                    TileBase tile = tilemap.GetTile(pos);
                    if (tile == null) continue;

                    Vector2Int chunkCoord = new(
                        Mathf.FloorToInt((float)x / chunkSize),
                        Mathf.FloorToInt((float)y / chunkSize)
                    );

                    if (!chunkDictionary.ContainsKey(chunkCoord))
                        //chunkDictionary[chunkCoord] = new TileBase[chunkSize, chunkSize];
                        chunkDictionary[chunkCoord] = new TileCell[chunkSize, chunkSize];

                    int localX = ((x % chunkSize) + chunkSize) % chunkSize;
                    int localY = ((y % chunkSize) + chunkSize) % chunkSize;

                    Matrix4x4 matrix = tilemap.GetTransformMatrix(pos);
                    Quaternion rot = matrix.rotation;
                    Vector3 scale = matrix.lossyScale;
                    Vector3 offset = matrix.MultiplyPoint3x4(Vector3.zero);

                    chunkDictionary[chunkCoord][localX, localY] = new TileCell
                    {
                        x = localX,
                        y = localY,
                        tileName = tile.name,
                        rotationZ = rot.eulerAngles.z,
                        offset = offset,
                        scale = scale
                    };
                    //chunkDictionary[chunkCoord][localX, localY] = tile;
                }
            }
            allChunkData[layerName] = chunkDictionary;
            Debug.Log($"Save chunk of layer: {layerName} ({chunkDictionary.Count} chunk)");
        }
        Debug.Log("Succes to extract from tilemap!");
    }

    [ContextMenu("Save Chunk Data To File")]
    public void SaveChunkDataToFile()
    {
        ExtractChunksFromAllTilemaps();
        string path = Path.Combine(Application.persistentDataPath, "chunkData.json");
        List<TileChunkData> export = new();

        foreach (var (layername, chunkDic) in allChunkData)
        {
            foreach (var (chunkCoord, tileArray) in chunkDic)
            {
                var chunk = new TileChunkData();
                chunk.layerName = layername;
                chunk.chunkX = chunkCoord.x;
                chunk.chunkY = chunkCoord.y;

                for (int x = 0; x < chunkSize; x++)
                {
                    for (int y = 0; y < chunkSize; y++)
                    {
                        //TileBase tile = tileArray[x, y];
                        //if (tile == null) continue;

                        //chunk.tiles.Add(new TileCell { x = x, y = y, tileName = tile.name });

                        var tile = tileArray[x, y];
                        if (tile == null) continue;
                        chunk.tiles.Add(tile);
                    }
                }

                export.Add(chunk);
            }    
        }    

        string json = JsonUtility.ToJson(new Wrapper<TileChunkData> { list = export }, true);
        File.WriteAllText(path, json);
        Debug.Log("Success to save chunk data!");
    }   
    
    [ContextMenu("Load Chunk Data From File")]
    public void LoadChunkDataFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "chunkData.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("File chunk data not found!"); 
            return;
        }

        var tileLibrary = tilePalette.ToDictionary(t => t.name, t => (TileBase)t);
        allChunkData.Clear();

        string json = File.ReadAllText(path);
        var wrapper = JsonUtility.FromJson<Wrapper<TileChunkData>>(json);

        foreach (var chunk in wrapper.list)
        {
            Vector2Int coord = new (chunk.chunkX, chunk.chunkY);
            if (!allChunkData.ContainsKey(chunk.layerName))
                allChunkData[chunk.layerName] = new();

            //var tileArray = new TileBase[chunkSize, chunkSize];
            var tileArray = new TileCell[chunkSize, chunkSize];

            foreach (var cell in chunk.tiles)
            {
                if (tileLibrary.TryGetValue(cell.tileName, out var tile))
                    //tileArray[cell.x, cell.y] = tile;
                    tileArray[cell.x, cell.y] = new TileCell
                    {
                        x = cell.x,
                        y = cell.y,
                        tileName = cell.tileName,
                        rotationZ = cell.rotationZ,
                        offset = cell.offset,
                        scale = cell.scale
                    };
            }

            allChunkData[chunk.layerName][coord] = tileArray;
        }

        Debug.Log("Success to load chunk data!");
    }    

    protected override void Start()
    {
        //ExtractChunksFromAllTilemaps();
        LoadChunkDataFromFile();
        foreach (var tilemap in gridRoot.GetComponentsInChildren<Tilemap>())
            tilemap.ClearAllTiles();

        foreach (var name in tilemapsByName.Keys)
            loadedChunksPerLayer[name] = new HashSet<Vector2Int>();
    }

    protected void Update()
    {
        LoadChunkAround();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        gridRoot = transform;
    }

    Vector2Int GetPlayerChunkCoord()
    {
        Vector3Int playerCell = tilemapsByName.Values.First().WorldToCell(player.position);
        return new Vector2Int(
            Mathf.FloorToInt(playerCell.x / chunkSize),
            Mathf.FloorToInt(playerCell.y / chunkSize)
        );
    }

    void LoadChunkAround()
    {
        if (tilemapsByName.Count == 0)
            tilemapsByName = gridRoot.GetComponentsInChildren<Tilemap>().ToDictionary(t => t.gameObject.name, t => t);

        Vector2Int playerChunk = GetPlayerChunkCoord();

        foreach (var layer in allChunkData.Keys)
        {
            var tilemap = tilemapsByName[layer];
            var chunkDict = allChunkData[layer];
            var loadedSet = loadedChunksPerLayer.ContainsKey(layer) ? loadedChunksPerLayer[layer] : (loadedChunksPerLayer[layer] = new());

            for (int x = -loadRadius; x <= loadRadius; x++)
            {
                for (int y = -loadRadius; y <= loadRadius; y++)
                {
                    Vector2Int coord = playerChunk + new Vector2Int(x, y);
                    if (!queuedChunk.Contains(coord))
                    {
                        chunkCoordQueue.Enqueue(coord);
                        queuedChunk.Add(coord);
                    }

                    if (!loadedChunksPerLayer.ContainsKey(layer))
                        loadedChunksPerLayer[layer] = new();

                    if (!loadedSet.Contains(coord))
                    {
                        loadedSet.Add(coord);
                    }
                }
            }

            var toUnload = new List<Vector2Int>();
            foreach (var coord in loadedSet)
            {
                if (Mathf.Abs(coord.x - playerChunk.x) > existRadius || Mathf.Abs(coord.y - playerChunk.y) > existRadius)
                {
                    UnloadChunk(tilemap, chunkDict, coord);
                    toUnload.Add(coord);
                    queuedChunk.Remove(coord);
                }
            }
            foreach (var c in toUnload) loadedSet.Remove(c);
        }

        if (!isLoadingChunks && chunkCoordQueue.Count > 0)
            StartCoroutine(LoadChunksGradully());
    }

    IEnumerator LoadChunksGradully()
    {
        isLoadingChunks = true;
        while (chunkCoordQueue.Count > 0)
        {
            var coord = chunkCoordQueue.Dequeue();
            foreach (var layer in allChunkData.Keys)
            {
                var tilemap = tilemapsByName[layer];
                var chunkDict = allChunkData[layer];

                LoadChunk(tilemap, chunkDict, coord);
                loadedChunksPerLayer[layer].Add(coord);
            }    

            yield return null;
        }    
        
        isLoadingChunks = false;
    }    

    void LoadChunk(Tilemap tilemap, Dictionary<Vector2Int, TileCell[,]> chunkDict, Vector2Int chunkCoord)
    {
        if (!chunkDict.ContainsKey(chunkCoord)) return;

        //TileBase[,] chunk = chunkDict[chunkCoord];
        TileCell[,] chunk = chunkDict[chunkCoord];
        int startX = chunkCoord.x * chunkSize;
        int startY = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                //TileBase tile = chunk[x, y];
                //if (tile != null)
                //    tilemap.SetTile(new Vector3Int(startX + x, startY + y, 0), tile);

                var cell = chunk[x, y];
                if (cell == null) continue;

                Vector3Int pos = new(startX + x, startY + y, 0);
                TileBase tile = tilePalette.FirstOrDefault(t => t.name == cell.tileName);
                if (tile == null) continue;

                tilemap.SetTile(pos, tile);

                Matrix4x4 matrix = Matrix4x4.TRS(
                    cell.offset,
                    Quaternion.Euler(0, 0, cell.rotationZ),
                    cell.scale == Vector3.zero ? Vector3.one : cell.scale
                );
                tilemap.SetTransformMatrix(pos, matrix);
            }
        }
    }

    void UnloadChunk(Tilemap tilemap, Dictionary<Vector2Int, TileCell[,]> chunkDict, Vector2Int chunkCoord)
    {
        //Debug.Log($"Unload chunk: {chunkCoord}");
        if (!chunkDict.ContainsKey(chunkCoord)) return;

        int startX = chunkCoord.x * chunkSize;
        int startY = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                tilemap.SetTile(new Vector3Int(startX + x, startY + y, 0), null);
            }
        }
    }
}

[Serializable]
public class TileChunkData
{
    public string layerName;
    public int chunkX;
    public int chunkY;
    public List<TileCell> tiles = new();
}
[Serializable]
public class TileCell
{
    public int x, y;
    public string tileName;
    public float rotationZ;
    public Vector3 offset;
    public Vector3 scale;
}

[Serializable]
public class Wrapper<T>
{
    public List<T> list;
}    


