using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeastStateEvent : EnemyStateEvent
{
    public void EnableAllRightHand()
    {
        colliers[0].gameObject.SetActive(true);
        colliers[1].gameObject.SetActive(true);
    }

    public void DisableAllRightHand()
    {
        colliers[0].gameObject.SetActive(false);
        colliers[1].gameObject.SetActive(false);
    }

    public void EnableAllLeftHand()
    {
        colliers[2].gameObject.SetActive(true);
        colliers[3].gameObject.SetActive(true);
    }

    public void DisableAllLeftHand()
    {
        colliers[2].gameObject.SetActive(false);
        colliers[3].gameObject.SetActive(false);
    }

    public void EnableLeftBackHand()
    {
        colliers[3].gameObject.SetActive(true);
    }

    public void DisableLeftBackHand()
    {
        colliers[3].gameObject.SetActive(false);
    }

    public void EnableAllBackHand()
    {
        colliers[1].gameObject.SetActive(true);
        colliers[3].gameObject.SetActive(true);
    }

    public void DisableAllBackHand()
    {
        colliers[1].gameObject.SetActive(false);
        colliers[3].gameObject.SetActive(false);
    }

    public void EnableFire()
    {
        colliers[4].gameObject.SetActive(true);
    }

    public void DisableFire()
    {
        colliers[4].gameObject.SetActive(false);
    }
}
