using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolderCtrl : KennMonoBehaviour
{
    public List<ObjectCtrl> holdObject;
    [SerializeField] protected int activeObjectAmount;

    public void LoadActiveObject()
    {
        int count = 0;
        int minus = 0;
        for(int i = 0; i < holdObject.Count; i++)
        {
            if (!holdObject[i].gameObject.activeSelf)
            {
                minus++;
                if(activeObjectAmount < 0) activeObjectAmount = 0;
            }
            count++;
        }
        activeObjectAmount = count;
        activeObjectAmount -= minus;
    }

    public void LoadHoldObject()
    {
        for (int i = holdObject.Count; i < transform.childCount; i++)
        {
            holdObject.Add(transform.GetChild(i).GetComponent<ObjectCtrl>());
        }
        LoadActiveObject();
    }

    public bool HasExistObject()
    {
        if (activeObjectAmount == 0) return false;
        return true;
    }

}
