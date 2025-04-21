using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnSystem : SpawnSystem
{
    private static ItemSpawnSystem instance;
    public static ItemSpawnSystem Instance => instance;
    [SerializeField] protected ItemSpawnerSample[] spawnerSamples;
    protected List<Transform> items = new List<Transform>();


    private void Awake()
    {
        CreateSingleton();
    }

    private void Update()
    {
        UpdateSpawnerHolder();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPrefabs("Item");
        LoadSpawnSample();
    }

    private void CreateSingleton()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("Exist Singleton " + instance.name);
    }

    protected void UpdateSpawnerHolder()
    {
        if (isSpawn)
        {
            isSpawn = false;
            holder.LoadHoldSpawner();
            foreach (var spawner in holder.holdSpawners)
            {
                spawner.Holder.LoadHoldObject();
            }
        }
    }

    protected void LoadSpawnSample()
    {
        spawnerSamples = Resources.LoadAll<ItemSpawnerSample>("SpawnerSamples");
    }

    public void SpawnItems(ItemType itemType, int amountSpawn, List<Vector2> positions)
    {
        ItemSpawnerSample newSpawner = CreateSpawner(itemType);
        newSpawner.Spawn(amountSpawn, positions);
    }

    public void SpawnItem(ItemType itemType, Vector2 positions)
    {
        ItemSpawnerSample newSpawner = CreateSpawner(itemType);
        newSpawner.Spawn(positions);
    }

    protected ItemSpawnerSample CreateSpawner(ItemType type)
    {
        ItemSpawnerSample ItemSpawner = null;
        List<Transform> spawnObjects = new List<Transform>();
        int length = prefabs.Length;
        for (int i = 0; i < length; i++)
        {
            Item item = prefabs[i].GetComponent<Item>();
            if (i == length - 1)
            {
                ItemSpawner = GetSpawnerFromHolder(type);
                ItemSpawner.SetSpawnObject(spawnObjects);
                switch (type)
                {
                    case ItemType.SpeedUp:
                        ItemSpawner.ItemType = ItemType.SpeedUp;
                        break;
                    case ItemType.SlowDown:
                        ItemSpawner.ItemType = ItemType.SlowDown;
                        break;
                    case ItemType.SuperPower:
                        ItemSpawner.ItemType = ItemType.SuperPower;
                        break;
                    case ItemType.MedicalKit:
                        ItemSpawner.ItemType = ItemType.MedicalKit;
                        break;
                    case ItemType.Armor:
                        ItemSpawner.ItemType = ItemType.Armor;
                        break;
                    case ItemType.GunAmmo:
                        ItemSpawner.ItemType = ItemType.GunAmmo;
                        break;
                    case ItemType.RiffleAmmo:
                        ItemSpawner.ItemType = ItemType.RiffleAmmo;
                        break;
                    case ItemType.FuelCanister:
                        ItemSpawner.ItemType = ItemType.FuelCanister;
                        break;
                    case ItemType.Money:
                        ItemSpawner.ItemType = ItemType.Money;
                        break;

                }
                ItemSpawner.name = type.ToString() + "Spawner";
                ItemSpawner.transform.parent = holder.transform;
            }
            if (item.Type == type)
            {
                spawnObjects.Add(prefabs[i]);
            }
        }
        return ItemSpawner;
    }

    protected ItemSpawnerSample GetSpawnerFromHolder(ItemType itemType)
    {
        foreach (var spawner in holder.holdSpawners)
        {
            ItemSpawnerSample itemSpawner = spawner.GetComponent<ItemSpawnerSample>();
            if (itemSpawner.ItemType == itemType) return itemSpawner;
        }

        ItemSpawnerSample newSpawner = Instantiate(spawnerSamples[0]);
        newSpawner.name = spawnerSamples[0].name;
        return newSpawner;
    }
}
