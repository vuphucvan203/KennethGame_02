using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ReactiveArmor,
    Rengeration,
    MuscleBoots,
    RapidFire,
    Flashstep
}

public class Skills : KennMonoBehaviour
{
    public Skill reactiveArmor;
    public Skill rengeration;
    public Skill muscleBoots;
    public Skill rapidFire;
    public Skill flashStep;
}

[Serializable]
public class Skill 
{
    public SkillType Type;
    public int skillPoint;
    protected int maxSkillPoints;

    public void UpdateSkill(Soldier soldier, int amount)
    {
        skillPoint += amount;
        switch (Type)
        {
            case SkillType.ReactiveArmor:
                soldier.DefenseStats.IncreaseStats(amount);
                break;
            case SkillType.Rengeration:
                soldier.Health.IncreaseStats(amount);
                break;
            case SkillType.MuscleBoots:
                soldier.AttackStats.IncreaseStats(amount);
                break;
            case SkillType.RapidFire:
                
                break;
            case SkillType.Flashstep:
                soldier.SpeedStats.IncreaseStats(amount);
                break;
        }
    }
}


