using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Jack,
    Linda,
    MindlessZombie,
    CopZombie,
    ArmyZombie,
    AcidSpitter,
    FleshThrower,
    AlphaBeast
}

[Serializable]
public class CharacterData
{
    public CharacterType type;
    public string name;
    public int health;
    public int attack;
    public int defense;
    public int speed;

    public CharacterData(CharacterType type, string name, int health, int attack, int defense, int speed)
    {
        this.type = type;
        this.name = name;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}

[Serializable]
public class SoldierData : CharacterData
{
    public int level;
    public int experience;

    public SoldierData(CharacterType type, string name, int level, int experience, int health, int attack, int defense, int speed) : base(type, name, health, attack, defense, speed)
    {
        this.level = level;
        this.experience = experience;
    }
}

[Serializable]
public class EnemyData : CharacterData
{
    public EnemyData(CharacterType type, string name, int health, int attack, int defense, int speed) : base(type, name, health, attack, defense, speed)
    {

    }
}

