using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MindlessZombieType
{
    AFemale,
    AMale,
    BFemale,
    BMale,
}

public class MindlessZombie : Enemy
{
    public MindlessZombie(string name) : base(name)
    {
        
    }
}
