using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FleshThrowerType
{
    A, 
    B,
}

public class FleshThrower : Enemy
{
    public FleshThrower(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadBaseStats(CharacterType.FleshThrower);
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
