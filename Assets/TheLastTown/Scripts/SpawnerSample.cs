using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerSample : KennMonoBehaviour
{
    [SerializeField] protected Transform prefab;
    [SerializeField] protected ObjectHolderCtrl holder;
    public ObjectHolderCtrl Holder => holder;
    [SerializeField] protected ObjectCtrl spawnObject;
    [SerializeField] protected List<Transform> poolingObject;
    protected int spawnCount;
    protected int spawnLimit;
    public bool isReused;

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

    protected Transform GetFromPoolingObject(Transform spawnObj)
    {
        foreach (Transform poolObj in poolingObject)
        {
            if(poolObj.name == spawnObj.name)
            {
                poolingObject.Remove(poolObj);
                return poolObj;
            }
        }

        Transform obj = Instantiate(spawnObj);
        obj.name = spawnObj.name;
        return obj;
    }

    public void SetSpawnObject(ObjectCtrl objectCtrl)
    {
        spawnObject = objectCtrl;
        ObjectCtrl newObject = Instantiate(objectCtrl);
        newObject.gameObject.SetActive(false);
        newObject.name = objectCtrl.EnemyType.ToString();
        newObject.transform.parent = prefab.transform;
    }

    public void DespawnObject(Transform obj)
    {
        poolingObject.Add(obj);
        obj.gameObject.SetActive(false);
        ObjectCtrl objCtrl = obj.GetComponent<ObjectCtrl>();
        objCtrl.isDeath = false;
        holder.LoadActiveObject();
    }
}
    