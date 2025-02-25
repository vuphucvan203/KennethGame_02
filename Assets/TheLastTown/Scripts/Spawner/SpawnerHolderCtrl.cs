using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHolderCtrl : KennMonoBehaviour
{
    public List<SpawnerSample> holdSpawners;

    public void LoadHoldSpawner()
    {
        for(int i = holdSpawners.Count; i < transform.childCount; i++)
        {
            holdSpawners.Add(transform.GetChild(i).GetComponent<SpawnerSample>());
        }
    }
}
