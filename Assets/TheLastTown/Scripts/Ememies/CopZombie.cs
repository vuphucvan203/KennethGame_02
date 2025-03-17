using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CopZombie : Enemy
{
    public CopZombie(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
