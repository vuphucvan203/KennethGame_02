using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnSystem : KennMonoBehaviour
{
    [SerializeField] protected List<Transform> poolingSpawner;
    [SerializeField] protected SpawnerHolderCtrl holder;
    [SerializeField] protected ObjectCtrl[] prefabs;
    public bool isSpawn;
    

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadHolder();
    }

    protected void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder").GetComponent<SpawnerHolderCtrl>();
    }

    protected virtual void LoadPrefabs(string nameFile)
    {
        prefabs = Resources.LoadAll<ObjectCtrl>("Prefabs/" + nameFile);
    }

    protected void PutSpawnerToPooling(Transform spawner)
    {
        Debug.Log("put to pooling");
        poolingSpawner.Add(spawner);
        spawner.gameObject.SetActive(false);
        SpawnerSample spawerSample = spawner.GetComponent<SpawnerSample>();
    }
}


