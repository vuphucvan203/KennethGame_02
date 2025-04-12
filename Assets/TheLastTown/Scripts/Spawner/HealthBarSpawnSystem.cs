using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBarSpawnSystem : SpawnSystem
{
    private static HealthBarSpawnSystem instance;
    public static HealthBarSpawnSystem Instance => instance;
    [SerializeField] protected HealthBarSpawnerSample[] spawnerSamples;
    protected List<Transform> healthBarSameType = new List<Transform>();


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
        LoadPrefabs("HealthBar");
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
        spawnerSamples = Resources.LoadAll<HealthBarSpawnerSample>("SpawnerSamples");
    }

    public void SpawnHealthBar(HealthBarType type, Character character)
    {
        HealthBarSpawnerSample newSpawner = CreateSpawner(type);
        newSpawner.Spawn(character);
    }

    protected HealthBarSpawnerSample CreateSpawner(HealthBarType type)
    {
        HealthBarSpawnerSample healthBarSpawner = null;
        int length = prefabs.Length;
        for (int i = 0; i < length; i++)
        {
            HealthBar healthBar = prefabs[i].GetComponent<HealthBar>();
            if (i == length - 1)
            {
                healthBarSpawner = GetSpawnerFromHolder(type);
                healthBarSpawner.SetSpawnObject(healthBarSameType);
                switch (type)
                {
                    case HealthBarType.S_EnemyHB:
                        healthBarSpawner.HealthBarType = HealthBarType.S_EnemyHB;
                        break;
                    case HealthBarType.M_EnemyHB:
                        healthBarSpawner.HealthBarType = HealthBarType.M_EnemyHB;
                        break;
                    case HealthBarType.XL_EnemyHB:
                        healthBarSpawner.HealthBarType = HealthBarType.XL_EnemyHB;
                        break;
                    case HealthBarType.XXL_EnemyHB:
                        healthBarSpawner.HealthBarType = HealthBarType.XXL_EnemyHB;
                        break;
                    case HealthBarType.XXXL_EnemyHB:
                        healthBarSpawner.HealthBarType = HealthBarType.XXXL_EnemyHB;
                        break;
                }
                healthBarSpawner.name = type.ToString() + "Spawner";
                healthBarSpawner.transform.SetParent(holder.transform);
            }
            if (healthBar.Type == type && !healthBarSameType.Contains(prefabs[i]))
            {
                healthBarSameType.Add(prefabs[i]);
            }
        }
        return healthBarSpawner;
    }

    protected HealthBarSpawnerSample GetSpawnerFromHolder(HealthBarType type)
    {
        foreach (var spawner in holder.holdSpawners)
        {
            HealthBarSpawnerSample healthBarSpawner = spawner.GetComponent<HealthBarSpawnerSample>();
            if(healthBarSpawner.HealthBarType == type) return healthBarSpawner;
        }

        HealthBarSpawnerSample newSpawner = Instantiate(spawnerSamples[0]);
        newSpawner.name = spawnerSamples[0].name;
        return newSpawner;
    }
}
