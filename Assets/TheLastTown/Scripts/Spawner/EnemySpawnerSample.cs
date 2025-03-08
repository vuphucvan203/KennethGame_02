using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSample : SpawnerSample
{
    [SerializeField] protected EnemyType enemyType;
    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }

    public List<Transform> Spawn(int amount, List<Vector2> positions)
    {
        List<Transform> groupEnemy = new List<Transform>();
        for (int i = 0; i < amount; i++)
        {
            Transform newEnemy = Spawn(positions[i]);
            groupEnemy.Add(newEnemy);   
            spawnCount++;
        }
        return groupEnemy;
    }

    public Transform Spawn(Vector2 position)
    {
        Transform newEnemy = GetFromPoolingObject(spawnObject.transform);
        newEnemy.parent = holder.transform.parent;
        newEnemy.position = position;
        newEnemy.gameObject.SetActive(true);
        newEnemy.transform.parent = holder.transform;
        spawnCount++;
        EnemySpawnSystem.Instance.isSpawn = true;
        return newEnemy;
    }
}
    