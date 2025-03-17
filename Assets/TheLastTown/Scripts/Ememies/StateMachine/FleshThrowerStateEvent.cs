using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshThrowerStateEvent : EnemyStateEvent
{
    public void EnableRightHand()
    {
        colliers[0].gameObject.SetActive(true);
    }
    
    public void DisableRightHand()
    {
        colliers[0].gameObject.SetActive(false);
    }

    public void EnableLeftHand()
    {
        colliers[1].gameObject.SetActive(true);
    }

    public void DisableLeftHand()
    {
        colliers[1].gameObject.SetActive(false);
    }

    public void EnableAllHand()
    {
        colliers[0].gameObject.SetActive(true);
        colliers[1].gameObject.SetActive(true);
    }

    public void DisableAllHand()
    {
        colliers[0].gameObject.SetActive(false);
        colliers[1].gameObject.SetActive(false);
    }

    public void EnableFlesh()
    {
        colliers[2].gameObject.SetActive(true);
    }

    public void DisableFlesh()
    {
        colliers[2].gameObject.SetActive(false);
    }
}
