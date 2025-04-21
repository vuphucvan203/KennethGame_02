using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerSample : SpawnerSample
{
    [SerializeField] protected ItemType itemType;
    public ItemType ItemType { get => itemType; set => itemType = value; }

    public List<Transform> Spawn(int amount, List<Vector2> positions)
    {
        List<Transform> groupItem = new List<Transform>();
        for (int i = 0; i < amount; i++)
        {
            Transform newItem = Spawn(positions[i]);
            groupItem.Add(newItem);   
            spawnCount++;
        }
        return groupItem;
    }

    public Transform Spawn(Vector2 position)
    {
        Transform newItem = GetFromPoolingObject(spawnObjects); 
        newItem.parent = holder.transform.parent;
        newItem.position = position;
        newItem.gameObject.SetActive(true);
        newItem.transform.SetParent(holder.transform);
        if (newItem.TryGetComponent<Money>(out var money)) money.startDelay = true;
        spawnCount++;
        ItemSpawnSystem.Instance.isSpawn = true;
        return newItem;
    }
}
    