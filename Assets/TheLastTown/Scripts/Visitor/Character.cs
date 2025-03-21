using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : KennMonoBehaviour
{
    protected string characterName;
    [SerializeField] protected BaseStats healthStats;
    public BaseStats Health => healthStats;
    [SerializeField] protected BaseStats attackStats;
    public BaseStats AttackStats => attackStats;
    [SerializeField] protected BaseStats defenseStats;
    public BaseStats DefenseStats => defenseStats;
    [SerializeField] protected BaseStats speedStats;
    public BaseStats SpeedStats => speedStats;
    public int currentDamage { get; set; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
    }

    public Character(string name, BaseStats health, BaseStats attack, BaseStats defense, BaseStats speed)
    {
        characterName = name;
        healthStats = health;
        attackStats = attack;
        defenseStats = defense;
        speedStats = speed;
    }

    protected void LoadBaseStats(CharacterType character)
    {
        CharacterData data = DataSystem.LoadSoldierData("/" + character.ToString() + "Data.json");
        healthStats = new BaseStats("Health", data.health, data.health);
        attackStats = new BaseStats("Attack", data.attack, 100);
        defenseStats = new BaseStats("Defense", data.defense, 100);
        speedStats = new BaseStats("Speed", data.speed, 8);
    }

    public int ApplyAttackStats(int damage)
    {
        float stats = damage * AttackStats.Percent;
        return (int)Mathf.Round(stats);
    } 

    public int ApplyDefenseStats(int damage)
    {
        float stats = damage * DefenseStats.Percent;
        return (int)Mathf.Round(stats);
    }    
        
    public abstract void Accept(IVisitor visitor);
}
