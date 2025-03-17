using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateEvent : EnemyStateEvent
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
}
