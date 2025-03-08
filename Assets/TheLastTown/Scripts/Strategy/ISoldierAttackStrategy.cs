using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider);
}

public class KnifeAttack : ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider)
    {
        State attack = soldider.StateMachine.Attack[0];
        soldider.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class BatAttack : ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider)
    {
        State attack = soldider.StateMachine.Attack[1];
        soldider.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class GunAttack : ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider)
    {
        State attack = soldider.StateMachine.Attack[2];
        soldider.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class RiffleAttack : ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider)
    {
        State attack = soldider.StateMachine.Attack[3];
        soldider.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class FlamethrowerAttack : ISoldierAttackStrategy
{
    public void SelectStrategy(Soldier soldider)
    {
        State attack = soldider.StateMachine.Attack[4];
        soldider.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}


