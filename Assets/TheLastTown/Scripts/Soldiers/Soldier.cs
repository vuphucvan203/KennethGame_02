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
    [SerializeField] protected int money;
    public int Money { get =>  money; set => money = value; }
    [SerializeField] protected Controller controller;
    public Controller Controller => controller;
    [SerializeField] protected SoldierStateTrigger stateTrigger;
    public SoldierStateTrigger StateTrigger => stateTrigger;
    [SerializeField] protected SoldierStateMachine stateMachine;
    public SoldierStateMachine StateMachine => stateMachine;
    [SerializeField] protected Rigidbody2D rig;
    public Rigidbody2D Rig => rig;
    public Inventory inventory;
    [SerializeField] protected BaseStats level;
    public BaseStats Level { get => level; set => level = value; }
    [SerializeField] protected BaseStats experience;
    public BaseStats Experience { get => experience; set => experience = value; }
    [SerializeField] protected Upgrade upgrade;
    public Upgrade Upgrade => upgrade;
    [SerializeField] protected Skills skills;
    public Skills Skills => skills;
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
        inventory = GetComponent<Inventory>();
        upgrade = GetComponent<Upgrade>();
        skills = GetComponent<Skills>();
        stateTrigger = GetComponent<SoldierStateTrigger>();
        stateMachine = GetComponent<SoldierStateMachine>();
        rig = GetComponent<Rigidbody2D>();
    }

    protected override void LoadBaseStats(CharacterType character)
    {
        SoldierData data = DataSystem.LoadSoldierData("/" + character.ToString() + "Data.json");
        healthStats = new BaseStats("Health", data.health, data.health);
        attackStats = new BaseStats("Attack", data.attack, 100);
        defenseStats = new BaseStats("Defense", data.defense, 100);
        speedStats = new BaseStats("Speed", data.speed, 8);
        level = new BaseStats("Level", data.level, 100);
        experience = new BaseStats("Experience", data.experience, 100);
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
