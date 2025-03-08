using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateMachine : StateMachine
{
    [SerializeField] protected Enemy enemy;
    public Enemy Enemy => enemy;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemy = GetComponent<Enemy>();
    }

    protected override void Start()
    {
        base.Start();
        enemy.StateTrigger.MoveTrigger += MoveHandle;
    }

    protected void MoveHandle()
    {
        enemy.StateTrigger.MoveTrigger -= MoveHandle;
        StartState(new EnemyMoveState(this));
    }
}
