using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateEvent : MonoBehaviour
{
    [SerializeField] protected SoldierStateMachine stateMachine;
    [SerializeField] protected WeaponCollider[] colliders;


    private void Awake()
    {
        stateMachine = GetComponentInParent<SoldierStateMachine>();
        LoadWeaponCollider();
    }

    protected void LoadWeaponCollider()
    {
        colliders = GetComponentsInChildren<WeaponCollider>();   
        foreach (WeaponCollider collider in colliders)
        {
            collider.gameObject.SetActive(false);
        }
    }    

    public void FinishAttack()
    {
        stateMachine.Soldider.StateTrigger.finishAttack = true;
    }    

    public void EnableKnife()
    {
        colliders[0].gameObject.SetActive(true);
    }

    public void DisableKnife()
    {
        colliders[0].gameObject.SetActive(false);
    }

    public void EnableBat()
    {
        colliders[1].gameObject.SetActive(true);
    }

    public void DisableBat()
    {
        colliders[1].gameObject.SetActive(false);
    }

    public void EnableFlamethrower()
    {
        colliders[2].gameObject.SetActive(true);
    }

    public void DisableFlamethrower()
    {
        colliders[2].gameObject.SetActive(false);
    }
}
