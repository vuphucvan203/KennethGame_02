using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linda : Soldier
{
    public Linda(string name, BaseStats level, BaseStats experience, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed) : base(name, level, experience, health, attack, defense, speed)
    {

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadBaseStats(CharacterType.Linda);
    }

    public override void Accept(IVisitor visitor)
    {   
        visitor.Visit(this);
    }
}
