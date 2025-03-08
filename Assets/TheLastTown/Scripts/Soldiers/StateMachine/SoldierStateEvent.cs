using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateEvent : MonoBehaviour
{
    [SerializeField] protected SoldierStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<SoldierStateMachine>();
    }

    public void FinishAttack()
    {
        stateMachine.Soldider.StateTrigger.finishAttack = true;
    }    
}
