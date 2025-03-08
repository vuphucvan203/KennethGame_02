using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStrategy
{
    public void SelectStrategy(Soldier soldider);
}

public class KnifeMove : IMoveStrategy
{
    void IMoveStrategy.SelectStrategy(Soldier soldider)
    {
        State move = soldider.StateMachine.Move[0];
        soldider.StateMachine.Animator.CrossFade(move.name, move.duringTime);
    }
}

public class BatMove : IMoveStrategy
{
    void IMoveStrategy.SelectStrategy(Soldier soldider)
    {
        State move = soldider.StateMachine.Move[1];
        soldider.StateMachine.Animator.CrossFade(move.name, move.duringTime);
    }
}

public class GunIMove : IMoveStrategy
{
    void IMoveStrategy.SelectStrategy(Soldier soldider)
    {
        State move = soldider.StateMachine.Move[2];
        soldider.StateMachine.Animator.CrossFade(move.name, move.duringTime);
    }
}

public class RiffleMove : IMoveStrategy
{
    void IMoveStrategy.SelectStrategy(Soldier soldider)
    {
        State move = soldider.StateMachine.Move[3];
        soldider.StateMachine.Animator.CrossFade(move.name, move.duringTime);
    }
}

public class FlamethrowerMove : IMoveStrategy
{
    void IMoveStrategy.SelectStrategy(Soldier soldider)
    {
        State move = soldider.StateMachine.Move[4];
        soldider.StateMachine.Animator.CrossFade(move.name, move.duringTime);
    }
}

