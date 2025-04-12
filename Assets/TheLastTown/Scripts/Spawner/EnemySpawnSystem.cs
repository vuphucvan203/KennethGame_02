using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnSystem : SpawnSystem
{
    private static EnemySpawnSystem instance;
    public static EnemySpawnSystem Instance => instance;
    [SerializeField] protected EnemySpawnerSample[] spawnerSamples;
    protected List<Transform> enemySameType = new List<Transform>();


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
        LoadPrefabs("Enemy");
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
        spawnerSamples = Resources.LoadAll<EnemySpawnerSample>("SpawnerSamples");
    }

    public void SpawnEnemies(EnemyType enemyType, int amountSpawn ,List<Vector2> positions)
    {
        EnemySpawnerSample newSpawner = CreateSpawner(enemyType);
        newSpawner.Spawn(amountSpawn, positions);
    }

    public void SpawnEnemy(EnemyType enemyType, Vector2 positions)
    {
        EnemySpawnerSample newSpawner = CreateSpawner(enemyType);
        newSpawner.Spawn(positions);
    }

    protected EnemySpawnerSample CreateSpawner(EnemyType type)
    {
        EnemySpawnerSample enemySpawner = null;
        int length = prefabs.Length;
        for (int i = 0; i < length; i++)
        {
            Enemy enemy = prefabs[i].GetComponent<Enemy>();
            if (i == length - 1)
            {
                enemySpawner = GetSpawnerFromHolder(type);
                enemySpawner.SetSpawnObject(enemySameType);
                switch (type)
                {
                    case EnemyType.MindlessZombie:
                        enemySpawner.EnemyType = EnemyType.MindlessZombie;
                        break;
                    case EnemyType.CopZombie:
                        enemySpawner.EnemyType = EnemyType.CopZombie;
                        break;
                    case EnemyType.ArmyZombie:
                        enemySpawner.EnemyType = EnemyType.ArmyZombie;
                        break;
                    case EnemyType.AcidSpitter:
                        enemySpawner.EnemyType = EnemyType.AcidSpitter;
                        break;
                    case EnemyType.FleshThrower:
                        enemySpawner.EnemyType = EnemyType.FleshThrower;
                        break;
                    case EnemyType.AlphaBeast:
                        enemySpawner.EnemyType = EnemyType.AlphaBeast;
                        break;
                }
                enemySpawner.name = type.ToString() + "Spawner";
                enemySpawner.transform.parent = holder.transform;
            }
            if (enemy.EnemyType == type)
            {
                enemySameType.Add(prefabs[i]);
            }
        }
        return enemySpawner;
    }

    protected EnemySpawnerSample GetSpawnerFromHolder(EnemyType enemyType)
    {
        foreach (var spawner in holder.holdSpawners)
        {
            EnemySpawnerSample enemySpawner = spawner.GetComponent<EnemySpawnerSample>();
            if(enemySpawner.EnemyType == enemyType) return enemySpawner;
        }

        EnemySpawnerSample newSpawner = Instantiate(spawnerSamples[0]);
        newSpawner.name = spawnerSamples[0].name;
        return newSpawner;
    }
}
