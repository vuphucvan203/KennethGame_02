using UnityEngine;

public class ItemSpawnTrigger : SpawnTrigger
{
    [SerializeField] protected ItemType type;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        coll = GetComponent<Collider2D>();
        spawnRange = GetComponentInChildren<SpawnRange>();  
    }

    protected override void ActiveSpawner()
    {
        ItemSpawnSystem.Instance.SpawnItems(type, spawnAmount, spawnRange.GetRandomSpawnPosition(spawnAmount));
    }
}
