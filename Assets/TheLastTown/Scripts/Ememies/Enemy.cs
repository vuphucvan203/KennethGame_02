using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    MindlessZombie,
    CopZombie,
    WarriorZombie,
    AcidSpitter,
    FleshThrower,
    AlphaMonstrosity
}

public abstract class Enemy : Character
{
    [SerializeField] protected EnemyAI enemyAI;
    public EnemyAI EnemyAI => enemyAI;
    [SerializeField] protected EnemyStateMachine stateMachine;
    public EnemyStateMachine StateMachine => stateMachine;
    [SerializeField] protected EnemyStateTrigger stateTrigger;
    public EnemyStateTrigger StateTrigger => stateTrigger;
    [SerializeField] protected IEnemyAttackStrategy attackStrategy;
    public AttackType currentAttack;

    protected Enemy(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    public IEnemyAttackStrategy AttackStrategy => attackStrategy;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemyAI = GetComponent<EnemyAI>();
        stateMachine = GetComponent<EnemyStateMachine>();
        stateTrigger = GetComponent<EnemyStateTrigger>();
    }

    public void SetStrategy(AttackType type)
    {
        switch(type)
        {
            case AttackType.Melee:
                attackStrategy = new MeleeAttack();
                break;
            case AttackType.SmallAcid:
                attackStrategy = new SmallAcidAttack();
                break;
            case AttackType.BigAcid:
                attackStrategy = new BigAcidAttack();
                break;
            case AttackType.Stun:
                attackStrategy = new StunAttack();
                break;
            case AttackType.Fire:
                attackStrategy = new FireAttack();
                break;
            case AttackType.BloodTalon:
                attackStrategy = new BloodTalonAttack();
                break;
            case AttackType.TwinTalons:
                attackStrategy = new TwinTalonsAttack();
                break;
        }
    }    
}
