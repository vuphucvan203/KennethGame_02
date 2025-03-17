using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] protected SoldierType soldierType;
    [SerializeField] protected CompanionAI companionAI;
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected AttackAction attackAction;
    [SerializeField] protected WeaponChange weaponChange;
    public WeaponChange WeaponChange => weaponChange;
    [SerializeField] protected PlayerHealthBar heathBar;
    protected Jack jack;
    protected Linda linda;
    

    private void Update()
    {
        WeaponUpdate();
        SelectSoldier();
        heathBar.SetSliderValue(soldier.Health.Percent);
        AttackExcute();
        MoveExcute();
        IdleExcute();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        jack = FindAnyObjectByType<Jack>();
        linda = FindAnyObjectByType<Linda>();
        companionAI = FindAnyObjectByType<CompanionAI>();
        joystick = FindAnyObjectByType<VariableJoystick>();
        attackAction = FindAnyObjectByType<AttackAction>();
        weaponChange = FindAnyObjectByType<WeaponChange>();
        heathBar = FindAnyObjectByType<PlayerHealthBar>();
    }

    protected void WeaponUpdate()
    {
        if (weaponChange.isChanged)
        {
            soldier.currentWeapon = WeaponChange.selectedWeapon;
            weaponChange.isChanged = false;
            changeWeapon = true;
        } 
    }    

    protected void SelectSoldier()
    {
        if (soldierType == SoldierType.Jack)
        {
            soldier = jack;
            soldier.SetController(ControllerType.Player);
            companionAI.SelectSoldier(linda);
        }
        else
        {
            soldier = linda;
            soldier.SetController(ControllerType.Player);
            companionAI.SelectSoldier(jack);
        }
    }    

    protected void MoveExcute()
    {
        if (joystick.Direction != Vector2.zero)
        {
            Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
            soldier.Rig.velocity = direction * 10f;
            if (direction.sqrMagnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                soldier.Rig.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
            soldier.StateTrigger.ActiveMove(weaponChange.selectedWeapon);
        }
        else soldier.Rig.velocity = Vector2.zero;
    }   
    
    protected void AttackExcute()
    {
        if(attackAction.isExcute)
        {
            attackAction.isExcute = false;
            soldier.StateTrigger.ActiveAttack(weaponChange.selectedWeapon);
        }
    }

    protected void IdleExcute()
    {
        if(joystick.Direction == Vector2.zero && !attackAction.isExcute)
        {
            soldier.StateTrigger.ActiveIdle(weaponChange.selectedWeapon);
        }    
    }

    public override void SelectSoldier(Soldier selectedSoldier)
    {
        
    }
}
