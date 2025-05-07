using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierStateTrigger : KennMonoBehaviour
{
    public delegate void IdleStrategy(WeaponType type);
    public IdleStrategy idleTrigger;
    public delegate void MoveStrategy(WeaponType type);
    public MoveStrategy moveTrigger;
    public delegate void AttackStrategy(WeaponType type);
    public AttackStrategy attackTrigger;
    public bool finishAttack { get; set; }

    public void ActiveIdle(WeaponType weapon)
    {
        idleTrigger?.Invoke(weapon);
    }  
    
    public void ActiveMove(WeaponType weapon)
    {
        moveTrigger?.Invoke(weapon);
    }  
    
    public void ActiveAttack(WeaponType weapon)
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;

        switch (weapon)
        {
            case WeaponType.Gun:
                BulletGun gun = weapons.FirstOrDefault(w => w.Type == WeaponType.Gun) as BulletGun;
                if (gun != null)
                {
                    if (gun.IsEmpty) return;
                    attackTrigger?.Invoke(weapon);
                }
                break;
            case WeaponType.Riffle:
                BulletGun riffle = weapons.FirstOrDefault(w => w.Type == WeaponType.Riffle) as BulletGun;
                if (riffle != null)
                {
                    if (riffle.IsEmpty) return;
                    attackTrigger?.Invoke(weapon);
                }
                break;
            case WeaponType.Flamethrower:
                Flamethrower flamethrower = weapons.FirstOrDefault(w => w.Type == WeaponType.Flamethrower) as Flamethrower;
                if (flamethrower != null)
                {
                    if (flamethrower.IsEmpty) return;
                    attackTrigger?.Invoke(weapon);
                }
                break;
            default:
                attackTrigger?.Invoke(weapon);
                break;
        } 
    }    
}
