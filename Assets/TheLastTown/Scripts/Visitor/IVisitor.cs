using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    public void Visit(Soldier soldier);
    public void Visit(Jack jack);
    public void Visit(Linda linda);
    public void Visit(Enemy enemy);
    public void Visit(MindlessZombie mindlessZombie);
    public void Visit(CopZombie copZombie);
    public void Visit(ArmyZombie armyZombie);
    public void Visit(AcidSpitter acidSpitter);
    public void Visit(FleshThrower fleshThrower);
    public void Visit(AlphaBeast alphaBeast);
}

public class CalculateDamageVisitor : IVisitor
{
    void IVisitor.Visit(Soldier soldier)
    {
        throw new System.NotImplementedException();
    }

    void IVisitor.Visit(Jack jack)
    {
        switch(jack.currentWeapon)
        {
            case WeaponType.Knife:
                jack.currentDamage = 5;
                break;
            case WeaponType.Bat:
                jack.currentDamage = 10;
                break;
            case WeaponType.Gun:
                jack.currentDamage = 15;
                break;
            case WeaponType.Riffle:
                jack.currentDamage = 20;
                break;
            case WeaponType.Flamethrower:
                jack.currentDamage = 25;
                break;
        }
        jack.currentDamage += jack.ApplyAttackStats(jack.currentDamage);
    }

    void IVisitor.Visit(Linda linda)
    {
        switch (linda.currentWeapon)
        {
            case WeaponType.Knife:
                linda.currentDamage = 5;
                break;
            case WeaponType.Bat:
                linda.currentDamage = 10;
                break;
            case WeaponType.Gun:
                linda.currentDamage = 15;
                break;
            case WeaponType.Riffle:
                linda.currentDamage = 20;
                break;
            case WeaponType.Flamethrower:
                linda.currentDamage = 25;
                break;
        }
        linda.currentDamage += linda.ApplyAttackStats(linda.currentDamage);
    }

    void IVisitor.Visit(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    void IVisitor.Visit(MindlessZombie mindlessZombie)
    {
        mindlessZombie.currentDamage = 10;
        mindlessZombie.currentDamage += mindlessZombie.ApplyAttackStats(mindlessZombie.currentDamage);
    }

    void IVisitor.Visit(CopZombie copZombie)
    {
        copZombie.currentDamage = 15;
        copZombie.currentDamage += copZombie.ApplyAttackStats(copZombie.currentDamage);
    }

    void IVisitor.Visit(ArmyZombie armyZombie)
    {
        armyZombie.currentDamage = 15;
        armyZombie.currentDamage += armyZombie.ApplyAttackStats(armyZombie.currentDamage);
    }

    void IVisitor.Visit(AcidSpitter acidSpitter)
    {
        switch (acidSpitter.currentAttack)
        {
            case AttackType.Melee:
                acidSpitter.currentDamage = 20;
                break;
            case AttackType.SmallAcid:
                acidSpitter.currentDamage = 25;
                break;
            case AttackType.BigAcid:
                acidSpitter.currentDamage = 25;
                break;
        }
        acidSpitter.currentDamage += acidSpitter.ApplyAttackStats(acidSpitter.currentDamage);
    }

    void IVisitor.Visit(FleshThrower fleshThrower)
    {
        switch (fleshThrower.currentAttack)
        {
            case AttackType.Melee:
                fleshThrower.currentDamage = 25;
                break;
            case AttackType.Stun:
                fleshThrower.currentDamage = 30;
                break;
        }
        fleshThrower.currentDamage += fleshThrower.ApplyAttackStats(fleshThrower.currentDamage);
    }

    void IVisitor.Visit(AlphaBeast alphaBeast)
    {
        switch (alphaBeast.currentAttack)
        {
            case AttackType.Melee:
                alphaBeast.currentDamage = 35;
                break;
            case AttackType.Fire:
                alphaBeast.currentDamage = 40;
                break;
            case AttackType.BloodTalon:
                alphaBeast.currentDamage = 45;
                break;
            case AttackType.TwinTalons:
                alphaBeast.currentDamage = 50;
                break;
        }
        alphaBeast.currentDamage += alphaBeast.ApplyAttackStats(alphaBeast.currentDamage);
    }
}

