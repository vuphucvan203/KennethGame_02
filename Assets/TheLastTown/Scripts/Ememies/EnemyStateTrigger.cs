using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateTrigger : KennMonoBehaviour
{
    public event Action MoveTrigger;
    public event Action AttackTrigger;

    public void ActiveMove()
    {
        MoveTrigger?.Invoke();
    }
    
    public void ActiveAttack()
    {
        AttackTrigger?.Invoke();
    }
}
