using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] protected Player player;
    public Player Player => player;
    [SerializeField] protected SoldierType soldierType;
    [SerializeField] protected CompanionAI companionAI;
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected AttackAction attackAction;
    //[SerializeField] protected WeaponChange weaponChange;
    //public WeaponChange WeaponChange => weaponChange;
    [SerializeField] protected ScrollWeapon scrollWeapon;
    public ScrollWeapon ScrollWeapon => scrollWeapon;
    [SerializeField] protected PlayerHealthBar heathBar;
    protected Jack jack;
    protected Linda linda;
    public float cooldownAttack;
    

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
        player = FindObjectOfType<Player>();
        jack = FindAnyObjectByType<Jack>();
        linda = FindAnyObjectByType<Linda>();
        companionAI = FindAnyObjectByType<CompanionAI>();
        joystick = FindAnyObjectByType<VariableJoystick>();
        attackAction = FindAnyObjectByType<AttackAction>();
        //weaponChange = FindAnyObjectByType<WeaponChange>();
        scrollWeapon = FindAnyObjectByType<ScrollWeapon>();
        heathBar = FindAnyObjectByType<PlayerHealthBar>();
    }

    protected void WeaponUpdate()
    {
        if (scrollWeapon.isChanged)
        {
            soldier.currentWeapon = ScrollWeapon.selectedWeapon;
            scrollWeapon.isChanged = false;
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
            soldier.StateTrigger.ActiveMove(scrollWeapon.selectedWeapon);
        }
        else soldier.Rig.velocity = Vector2.zero;
    }   
    
    protected void AttackExcute()
    {
        if (attackAction.isExcute)
        {
            attackAction.isExcute = false;
            soldier.StateTrigger.ActiveAttack(scrollWeapon.selectedWeapon);
        }
    }

    protected void IdleExcute()
    {
        if(joystick.Direction == Vector2.zero && !attackAction.isExcute)
        {
            soldier.StateTrigger.ActiveIdle(scrollWeapon.selectedWeapon);
        }    
    }

    public override void SelectSoldier(Soldier selectedSoldier)
    {
        
    }
}
