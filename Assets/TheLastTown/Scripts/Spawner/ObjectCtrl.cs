using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCtrl : KennMonoBehaviour
{
    [SerializeField] protected EnemyType enemyType;
    public EnemyType EnemyType { get => enemyType; }

    [SerializeField] protected SpawnerSample spawner;

    public bool isDeath;

    private void Update()
    {
        if (!isDeath) return;
        spawner.DespawnObject(transform);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadSpawner();
    }

    protected void LoadSpawner()
    {
        if (spawner != null) return;
        spawner = GetComponentInParent<SpawnerSample>();
    }
}
