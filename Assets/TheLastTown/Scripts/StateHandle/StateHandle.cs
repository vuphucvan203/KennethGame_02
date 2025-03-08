using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "StateSO/newStateHandle")]
public class StateHandle : ScriptableObject
{
    public State[] Idle;
    public State[] Move;
    public State[] Attack;
    public State[] Death;
}

[Serializable]
public class State
{
    public string name;
    public float duringTime;

    public override string ToString()
    {
        return name;
    }
}

