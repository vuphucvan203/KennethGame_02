using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Knife,
    Bat,
    Gun, 
    Riffle,
    Flamethrower,
}

public class PlayerController : KennMonoBehaviour
{
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected AttackAction attackAction;
    [SerializeField] protected WeaponChange weaponChange;
    public WeaponChange WeaponChange => weaponChange;
    [SerializeField] protected Rigidbody2D rig;
    public delegate void IdleStrategy(WeaponType type);
    public IdleStrategy idleTrigger;
    public delegate void MoveStrategy(WeaponType type);
    public MoveStrategy moveTrigger;
    public delegate void AttackStrategy(WeaponType type);
    public AttackStrategy attackTrigger;
    public bool finishAttack;

    private void Update()
    {
        AttackExcute();
        MoveExcute();
        IdleExcute();
    }

    protected void MoveExcute()
    {
        if (joystick.Direction != Vector2.zero)
        {
            Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
            rig.velocity = direction * 10f;
            moveTrigger?.Invoke(weaponChange.selectedWeapon);
        }
        else rig.velocity = Vector2.zero;
    }   
    
    protected void AttackExcute()
    {
        if(attackAction.isExcute)
        {
            attackAction.isExcute = false;
            attackTrigger?.Invoke(weaponChange.selectedWeapon);
        }
    }

    protected void IdleExcute()
    {
        if(joystick.Direction == Vector2.zero && !attackAction.isExcute)
        {
            idleTrigger?.Invoke(weaponChange.selectedWeapon);
        }    
    }    
}
