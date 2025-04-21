using UnityEngine;

public class EnemySpawnTrigger : SpawnTrigger
{
    [SerializeField] protected EnemyType type;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        coll = GetComponent<Collider2D>();
        spawnRange = GetComponentInChildren<SpawnRange>();  
    }

    protected override void ActiveSpawner()
    {
        EnemySpawnSystem.Instance.SpawnEnemies(type, spawnAmount, spawnRange.GetRandomSpawnPosition(spawnAmount));
    }
}
