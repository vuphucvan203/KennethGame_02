using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmyZombie : Enemy
{
    public ArmyZombie(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadBaseStats(CharacterType.ArmyZombie);
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
