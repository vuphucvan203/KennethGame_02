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
    private Dictionary<string, Dictionary<Vector2Int, TileBase[,]>> allChunkData = new();
    private Dictionary<string, HashSet<Vector2Int>> loadedChunksPerLayer = new();
    private Queue<(string layerName, Vector2Int coord)> chunkLoadQueue = new();
    private bool isLoadingChunks = false;

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

    [ContextMenu("Extract Chunks From Tilemaps")]
    public void ExtractChunksFromAllTilemaps()
    {
        tilemapsByName.Clear();
        allChunkData.Clear();

        var tilemaps = gridRoot.GetComponentsInChildren<Tilemap>();

        foreach (var tilemap in tilemaps)
        {
            string layerName = tilemap.gameObject.name;
            tilemapsByName[layerName] = tilemap;

            var chunkDictionary = new Dictionary<Vector2Int, TileBase[,]>();
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
                        chunkDictionary[chunkCoord] = new TileBase[chunkSize, chunkSize];

                    int localX = ((x % chunkSize) + chunkSize) % chunkSize;
                    int localY = ((y % chunkSize) + chunkSize) % chunkSize;
                    chunkDictionary[chunkCoord][localX, localY] = tile;
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
                        TileBase tile = tileArray[x, y];
                        if (tile == null) continue;

                        chunk.tiles.Add(new TileCell { x = x, y = y, tileName = tile.name });
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

            var tileArray = new TileBase[chunkSize, chunkSize];

            foreach (var cell in chunk.tiles)
            {
                if (tileLibrary.TryGetValue(cell.tileName, out var tile))
                    tileArray[cell.x, cell.y] = tile;
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

    Vector2Int GetPlayerChunkCoord(Tilemap tilemap)
    {
        Vector3Int playerCell = tilemap.WorldToCell(player.position);
        return new Vector2Int(
            Mathf.FloorToInt(playerCell.x / chunkSize),
            Mathf.FloorToInt(playerCell.y / chunkSize)
        );
    }

    void LoadChunkAround()
    {
        //HashSet<Vector2Int> shouldExist = new();
        if (tilemapsByName.Count == 0)
            tilemapsByName = gridRoot.GetComponentsInChildren<Tilemap>().ToDictionary(t => t.gameObject.name, t => t);

        foreach (var layer in allChunkData.Keys)
        {
            var tilemap = tilemapsByName[layer];
            var chunkDict = allChunkData[layer];
            var loadedSet = loadedChunksPerLayer.ContainsKey(layer) ? loadedChunksPerLayer[layer] : (loadedChunksPerLayer[layer] = new());

            Vector2Int playerChunk = GetPlayerChunkCoord(tilemap);
            for (int x = -loadRadius; x <= loadRadius; x++)
            {
                for (int y = -loadRadius; y <= loadRadius; y++)
                {
                    Vector2Int coord = playerChunk + new Vector2Int(x, y);
                    //shouldExist.Add(coord);
                    if (!loadedSet.Contains(coord))
                    {
                        chunkLoadQueue.Enqueue((layer, coord));
                        loadedSet.Add(coord);
                    }
                }
            }

            var toUnload = new List<Vector2Int>();
            foreach (var coord in loadedSet)
            {
                //if (!shouldExist.Contains(coord))
                if (Mathf.Abs(coord.x - playerChunk.x) > existRadius || Mathf.Abs(coord.y - playerChunk.y) > existRadius)
                {
                    UnloadChunk(tilemap, chunkDict, coord);
                    toUnload.Add(coord);
                }
            }
            foreach (var c in toUnload) loadedSet.Remove(c);
        }

        if (!isLoadingChunks && chunkLoadQueue.Count > 0)
            StartCoroutine(LoadChunksGradully());
    }

    //void PreviewAllChunks()
    //{
    //    foreach (var layer in allChunkData.Keys)
    //    {
    //        var tilemap = tilemapsByName[layer];
    //        var chunkDict = allChunkData[layer];

    //        foreach (var coord in chunkDict.Keys)
    //        {
    //            LoadChunk(tilemap, chunkDict, coord);
    //        }

    //        Debug.Log($" Preview all chunks: {layer}");
    //    }
    //}

    IEnumerator LoadChunksGradully()
    {
        isLoadingChunks = true;
        while (chunkLoadQueue.Count > 0)
        {
            var (layer, coord) = chunkLoadQueue.Dequeue();
            var tilemap = tilemapsByName[layer];
            var chunkDict = allChunkData[layer];

            LoadChunk(tilemap, chunkDict, coord);
            loadedChunksPerLayer[layer].Add(coord);

            yield return null;
        }    
        
        isLoadingChunks = false;
    }    

    void LoadChunk(Tilemap tilemap, Dictionary<Vector2Int, TileBase[,]> chunkDict, Vector2Int chunkCoord)
    {
        if (!chunkDict.ContainsKey(chunkCoord)) return;

        TileBase[,] chunk = chunkDict[chunkCoord];
        int startX = chunkCoord.x * chunkSize;
        int startY = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                TileBase tile = chunk[x, y];
                if (tile != null)
                    tilemap.SetTile(new Vector3Int(startX + x, startY + y, 0), tile);
            }
        }
    }

    void UnloadChunk(Tilemap tilemap, Dictionary<Vector2Int, TileBase[,]> chunkDict, Vector2Int chunkCoord)
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
}

[Serializable]
public class Wrapper<T>
{
    public List<T> list;
}    


