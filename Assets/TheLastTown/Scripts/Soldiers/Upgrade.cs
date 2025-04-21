using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;

    protected override void Start()
    {
        soldier.Level.value = 0;
        soldier.Experience = new BaseStats("Experience", 0, 100);
    }

    protected void Update()
    {
        if (soldier.Experience.overValue)
        {
            LevelUp(soldier.Level.value + 1);
        }    
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        soldier = GetComponent<Soldier>();
    }

    public void LevelUp(int level)
    {
        soldier.Level = new BaseStats("Level", level, 100);
        int remaining = soldier.Experience.Remaining;
        soldier.Experience = new BaseStats("Experience", remaining, level * 150);
    }   
    
    public void UpgradeSkill(SkillType type, int skillPoint)
    {
        switch (type)
        {
            case SkillType.ReactiveArmor:
                soldier.Skills.reactiveArmor.UpdateSkill(soldier, skillPoint);
                break;
            case SkillType.Rengeration:
                soldier.Skills.rengeration.UpdateSkill(soldier, skillPoint);
                break;
            case SkillType.MuscleBoots:
                soldier.Skills.muscleBoots.UpdateSkill(soldier, skillPoint);
                break;
            case SkillType.RapidFire:
                soldier.Skills.rapidFire.UpdateSkill(soldier, skillPoint);
                break;
            case SkillType.Flashstep:
                soldier.Skills.flashStep.UpdateSkill(soldier, skillPoint);
                break;  
        }    
    }
}
