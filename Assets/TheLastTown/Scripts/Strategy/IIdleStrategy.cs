using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdleStrategy
{
    public void SelectStrategy(Soldier soldider);

    public void SelectStrategy(Jack jack);
}

public class KnifeIdle : IIdleStrategy
{
    public void SelectStrategy(Jack jack)
    {
        jack.StateMachine.Animator.CrossFade("J_Idle_knife", 0.2f);
    }

    void IIdleStrategy.SelectStrategy(Soldier soldider)
    {
        State idle = soldider.StateMachine.Idle[0];
        soldider.StateMachine.Animator.CrossFade(idle.name, idle.duringTime);
    }
}

public class BatIdle : IIdleStrategy
{
    public void SelectStrategy(Jack jack)
    {
        jack.StateMachine.Animator.CrossFade("J_Idle_bat", 0.2f);
    }

    void IIdleStrategy.SelectStrategy(Soldier soldider)
    {
        State idle = soldider.StateMachine.Idle[1];
        soldider.StateMachine.Animator.CrossFade(idle.name, idle.duringTime);
    }
}

public class GunIdle : IIdleStrategy
{
    public void SelectStrategy(Jack jack)
    {
        jack.StateMachine.Animator.CrossFade("J_Idle_gun", 0.2f);
    }

    void IIdleStrategy.SelectStrategy(Soldier soldider)
    {
        State idle = soldider.StateMachine.Idle[2];
        soldider.StateMachine.Animator.CrossFade(idle.name, idle.duringTime);
    }
}

public class RiffleIdle : IIdleStrategy
{
    public void SelectStrategy(Jack jack)
    {
        jack.StateMachine.Animator.CrossFade("J_Idle_riffle", 0.2f);
    }

    void IIdleStrategy.SelectStrategy(Soldier soldider)
    {
        State idle = soldider.StateMachine.Idle[3];
        soldider.StateMachine.Animator.CrossFade(idle.name, idle.duringTime);
    }
}

public class FlamethrowerIdle : IIdleStrategy
{
    public void SelectStrategy(Jack jack)
    {
        jack.StateMachine.Animator.CrossFade("J_Idle_flamethrower", 0.2f);
    }

    void IIdleStrategy.SelectStrategy(Soldier soldider)
    {
        State idle = soldider.StateMachine.Idle[4];
        soldider.StateMachine.Animator.CrossFade(idle.name, idle.duringTime);
    }
}

