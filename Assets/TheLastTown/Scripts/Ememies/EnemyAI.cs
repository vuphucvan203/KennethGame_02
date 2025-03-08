using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateAction
{
    Attack,
    Move,
}

public enum AttackType
{
    Melee,
    SmallAcid,
    BigAcid,
    Stun,
    Fire,
    BloodTalon,
    TwinTalons
}

public abstract class EnemyAI : KennMonoBehaviour
{ 
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected EnemyStateTrigger stateTrigger;
    [SerializeField] protected StateAction action;
    [SerializeField] protected AttackType attack;
    public AttackType AttackType => attack;
    [SerializeField] protected float timer, cooldown;
    public bool startCooldown { get; set; }

    protected override void Start()
    {
        base.Start();
        action = StateAction.Move;
    }

    private void Update()
    {
        MakeDecision();
        MoveExcute();
        AttackExcute();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemy = GetComponent<Enemy>();
        stateTrigger = GetComponent<EnemyStateTrigger>();
    }

    protected virtual void MakeDecision()
    {
        StartCooldown(cooldown);
    }    

    protected void MoveExcute()
    {
        if (action == StateAction.Move) stateTrigger.ActiveMove();
    }

    protected void AttackExcute()
    {
        if (action== StateAction.Attack) stateTrigger.ActiveAttack();
    }

    public void StartCooldown(float cooldown)
    {
        if (startCooldown)
        {
            action = StateAction.Move;
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                action = StateAction.Attack;
                startCooldown = false;
                timer = 0;
            }
        }
    }
}
