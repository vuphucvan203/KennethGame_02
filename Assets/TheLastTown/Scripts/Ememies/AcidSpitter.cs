using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AcidSpitterType
{
    A, 
    B,
}

public class AcidSpitter : Enemy
{
    public AcidSpitter(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadBaseStats(CharacterType.AcidSpitter);
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
