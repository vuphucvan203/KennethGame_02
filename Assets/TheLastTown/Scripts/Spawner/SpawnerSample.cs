using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerSample : KennMonoBehaviour
{
    [SerializeField] protected Transform prefab;
    [SerializeField] protected ObjectHolderCtrl holder;
    public ObjectHolderCtrl Holder => holder;
    [SerializeField] protected List<Transform> spawnObjects;
    [SerializeField] protected List<Transform> poolingObject;
    protected int spawnCount;
    protected int spawnLimit;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPrefab();
        LoadHolder();
    }

    protected void LoadPrefab()
    {
        if (prefab != null) return;
        prefab = transform.Find("Prefab").GetComponent<Transform>();
    }

    protected void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder").GetComponent<ObjectHolderCtrl>();
    }

    protected Transform GetFromPoolingObject(List<Transform> spawnObjects)
    {
        int index = Random.Range(0, spawnObjects.Count - 1);

        foreach (Transform poolObj in poolingObject)
        {
            if(poolObj.name == spawnObjects[index].name)
            {
                poolingObject.Remove(poolObj);
                return poolObj;
            }
        }

        Transform obj = Instantiate(spawnObjects[index]);
        obj.name = spawnObjects[index].name;
        return obj;
    }

    public void SetSpawnObject(List<Transform> characters)
    {
        spawnObjects = characters;
        //foreach (Transform transform in characters)
        //{
        //    Transform newObject = Instantiate(transform);
        //    newObject.gameObject.SetActive(false);
        //    newObject.name = transform.transform.name;
        //    newObject.SetParent(prefab.transform);
        //}    
    }

    public void DespawnObject(Transform obj)
    {
        poolingObject.Add(obj);
        obj.gameObject.SetActive(false);
        holder.LoadActiveObject();
    }
}
    