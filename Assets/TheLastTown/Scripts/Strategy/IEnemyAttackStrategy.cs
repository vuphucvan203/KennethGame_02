using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy);
}

public class MeleeAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[0];
        //enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
        enemy.StateMachine.Animator.Play(attack.name);
    }
}

public class SmallAcidAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[1];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class BigAcidAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[2];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class StunAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[1];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class FireAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[1];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class BloodTalonAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[2];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}

public class TwinTalonsAttack : IEnemyAttackStrategy
{
    public void SelectStrategy(Enemy enemy)
    {
        State attack = enemy.StateMachine.Attack[3];
        enemy.StateMachine.Animator.CrossFade(attack.name, attack.duringTime);
    }
}



