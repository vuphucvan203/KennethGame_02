using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnSystem : SpawnSystem
{
    private static EnemySpawnSystem instance;
    public static EnemySpawnSystem Instance => instance;
    [SerializeField] protected EnemySpawnerSample[] spawnerSamples;


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

    protected EnemySpawnerSample CreateSpawner(EnemyType enemyType)
    {
        EnemySpawnerSample enemySpawner = null;

        foreach (ObjectCtrl enemyCtrl in prefabs)
        {
            if (enemyCtrl.EnemyType == enemyType)
            {
                enemySpawner = GetSpawnerFromHolder(enemyType);
                enemySpawner.SetSpawnObject(enemyCtrl);

                switch (enemyType)
                {
                    case EnemyType.Monster:
                        enemySpawner.EnemyType = EnemyType.Monster;
                        break;
                    case EnemyType.Zombie:
                        enemySpawner.EnemyType = EnemyType.Zombie;
                        break;
                }
                enemySpawner.name = enemyCtrl.name + "Spawner";
                enemySpawner.transform.parent = holder.transform;
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

public enum EnemyType
{
    Zombie,
    Monster,
}
