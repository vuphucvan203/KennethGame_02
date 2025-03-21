using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType
{
    Jack,
    Linda,
}

public abstract class Soldier : Character
{
    [SerializeField] protected Controller controller;
    public Controller Controller => controller;
    [SerializeField] protected SoldierStateTrigger stateTrigger;
    public SoldierStateTrigger StateTrigger => stateTrigger;
    [SerializeField] protected SoldierStateMachine stateMachine;
    public SoldierStateMachine StateMachine => stateMachine;
    [SerializeField] protected Rigidbody2D rig;
    public Rigidbody2D Rig => rig;
    protected BaseStats level;
    protected BaseStats experience;
    protected IIdleStrategy idleStrategy;
    public IIdleStrategy IdleStrategy => idleStrategy;
    protected IMoveStrategy moveStrategy;
    public IMoveStrategy MoveStrategy => moveStrategy;
    protected ISoldierAttackStrategy attackStrategy;
    public ISoldierAttackStrategy AttackStrategy => attackStrategy;
    public WeaponType currentWeapon { get; set; }


    protected Soldier(string name, BaseStats level, BaseStats experience, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {
        this.level = level;
        this.experience = experience;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        stateTrigger = GetComponent<SoldierStateTrigger>();
        stateMachine = GetComponent<SoldierStateMachine>();
        rig = GetComponentInChildren<SpriteRenderer>().GetComponent<Rigidbody2D>();
    }  

    public void SetStategy(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Knife:
                idleStrategy = new KnifeIdle();
                moveStrategy = new KnifeMove();
                attackStrategy = new KnifeAttack();
                break;
            case WeaponType.Bat:
                idleStrategy = new BatIdle();
                moveStrategy = new BatMove();
                attackStrategy = new BatAttack();
                break;
            case WeaponType.Gun:
                idleStrategy = new GunIdle();
                moveStrategy = new GunIMove();
                attackStrategy = new GunAttack();
                break;
            case WeaponType.Riffle:
                idleStrategy = new RiffleIdle();
                moveStrategy = new RiffleMove();
                attackStrategy = new RiffleAttack();
                break;
            case WeaponType.Flamethrower:
                idleStrategy = new FlamethrowerIdle();
                moveStrategy = new FlamethrowerMove();
                attackStrategy = new FlamethrowerAttack();
                break;
        }
    }

    public void SetController(ControllerType ctrl)
    {
        if (ctrl == ControllerType.Player)
        {
            controller = FindAnyObjectByType<PlayerController>();
        }
        else controller = FindAnyObjectByType<CompanionAI>();
    }    

    public virtual void Attack(WeaponType weapon)
    {

    }    
}
