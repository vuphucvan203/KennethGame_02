using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField] protected SoldierStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<SoldierStateMachine>();
    }

    public void FinishAttack()
    {
        stateMachine.Controller.finishAttack = true;
    }    
}
