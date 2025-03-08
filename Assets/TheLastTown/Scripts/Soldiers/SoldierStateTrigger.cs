using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateTrigger : KennMonoBehaviour
{
    public delegate void IdleStrategy(WeaponType type);
    public IdleStrategy idleTrigger;
    public delegate void MoveStrategy(WeaponType type);
    public MoveStrategy moveTrigger;
    public delegate void AttackStrategy(WeaponType type);
    public AttackStrategy attackTrigger;
    public bool finishAttack { get; set; }

    public void ActiveIdle(WeaponType weapon)
    {
        idleTrigger?.Invoke(weapon);
    }  
    
    public void ActiveMove(WeaponType weapon)
    {
        moveTrigger?.Invoke(weapon);
    }  
    
    public void ActiveAttack(WeaponType weapon)
    {
        attackTrigger?.Invoke(weapon);
    }    
}
