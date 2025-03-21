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
    public MindlessZombie(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, health, attack, defense, speed)
    {

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadBaseStats(CharacterType.MindlessZombie);
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
