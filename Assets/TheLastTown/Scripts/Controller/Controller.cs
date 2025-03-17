using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Knife,
    Bat,
    Gun,
    Riffle,
    Flamethrower,
}

public enum ControllerType
{
    Player,
    AI,
}

public abstract class Controller : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;
    public Soldier Soldier => soldier;
    public bool changeWeapon;

    public abstract void SelectSoldier(Soldier selectedSoldier);
}
