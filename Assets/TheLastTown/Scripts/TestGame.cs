using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGame : KennMonoBehaviour
{
    public bool spawn;
    public SpawnRange spawnRange;
    public int amount;

    protected override void Start()
    {
        //TestSpawner();
    }

    private void Update()
    {
        if(spawn)
        {
            spawn = false;
            TestSpawner(amount);
        }
    }

    protected void TestSpawner(int amount)
    {
        List<Vector2> listPos = spawnRange.GetRandomSpawnPosition(amount);
        EnemySpawnSystem.Instance.SpawnEnemies(EnemyType.Monster, amount, listPos);
    }

    protected void TestOther()
    {
        Vector2 pos = new Vector2(1, -3);
        EnemySpawnSystem.Instance.SpawnEnemy(EnemyType.Zombie, pos);
    }
}
