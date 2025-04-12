using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChunkGenerator : MonoBehaviour
{
    [Header("Setup")]
    public Transform gridRoot; 
    public GameObject tilemapChunkPrefab; 
    public Transform chunkParent; 
    public int chunkSize = 64; 

    [ContextMenu("Generate Tilemap Chunks")]
    public void GenerateChunks()
    {
        if (gridRoot == null || tilemapChunkPrefab == null || chunkParent == null)
        {
            Debug.LogError("Missing setup!");
            return;
        }

        Tilemap[] tilemaps = gridRoot.GetComponentsInChildren<Tilemap>();

        Dictionary<string, int> sortingOrders = new Dictionary<string, int>();
        Dictionary<string, int> sortingLayers = new Dictionary<string, int>();
        Dictionary<string, bool> hasCollider = new Dictionary<string, bool>();

        foreach (var tilemap in tilemaps)
        {
            string layerName = tilemap.gameObject.name;

            var renderer = tilemap.GetComponent<TilemapRenderer>();
            sortingOrders[layerName] = renderer.sortingOrder;
            sortingLayers[layerName] = renderer.sortingLayerID;

            var col = tilemap.GetComponent<TilemapCollider2D>();
            hasCollider[layerName] = col != null;
        }

        foreach (var tilemap in tilemaps)
        {
            string layerName = tilemap.gameObject.name;
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++) 
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile == null) continue;

                    int tileX = x + bounds.xMin;
                    int tileY = y + bounds.yMin;

                    int chunkX = Mathf.FloorToInt((float) tileX / chunkSize);
                    int chunkY = Mathf.FloorToInt((float) tileY / chunkSize);
                    string chunkName = $"Chunk_{chunkX}_{chunkY}";

                    Transform chunkTransform = chunkParent.Find(chunkName);
                    GameObject chunkGameObject;
                    if (chunkTransform == null)
                    {
                        chunkGameObject = Instantiate(tilemapChunkPrefab, chunkParent);
                        chunkGameObject.name = chunkName;
                        chunkGameObject.transform.position = Vector3.zero;
                    }
                    else chunkGameObject = chunkTransform.gameObject;

                    Transform layerTransform = chunkGameObject.transform.Find(layerName);
                    Tilemap chunkTilemap;

                    if (layerTransform == null)
                    {
                        GameObject layerGameObject = new GameObject(layerName);
                        layerGameObject.transform.parent = chunkGameObject.transform;
                        layerGameObject.transform.localPosition = Vector3.zero;

                        chunkTilemap = layerGameObject.AddComponent<Tilemap>();
                        var render = layerGameObject.AddComponent<TilemapRenderer>();
                        render.sortingOrder = sortingOrders[layerName];
                        render.sortingLayerID = sortingLayers[layerName];

                        if (hasCollider[layerName])
                        {
                            layerGameObject.AddComponent<TilemapCollider2D>();
                        }
                    }   
                    else chunkTilemap = layerTransform.GetComponent<Tilemap>();

                    chunkTilemap.SetTile(new Vector3Int(tileX, tileY), tile);
                }
            }
        }
        Debug.Log("Success to generate chunk!");
    }
}
