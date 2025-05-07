using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoldierStateEvent : MonoBehaviour
{
    [SerializeField] protected SoldierStateMachine stateMachine;
    [SerializeField] protected WeaponCollider[] colliders;
    [SerializeField] protected WeaponRaycast[] raycasts;


    private void Awake()
    {
        stateMachine = GetComponentInParent<SoldierStateMachine>();
        LoadWeaponCollider();
        LoadWeaponRaycast();
    }

    protected void LoadWeaponCollider()
    {
        colliders = GetComponentsInChildren<WeaponCollider>();   
        foreach (WeaponCollider collider in colliders)
        {
            collider.gameObject.SetActive(false);
        }
    }    

    protected void LoadWeaponRaycast()
    {
        raycasts = GetComponentsInChildren<WeaponRaycast>();
        foreach (WeaponRaycast raycast in raycasts)
        {
            raycast.gameObject.SetActive(false);
        }    
    }    

    public void FinishAttack()
    {
        stateMachine.Soldider.StateTrigger.finishAttack = true;
    }    

    public void EnableKnife()
    {
        colliders[0].gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXEffect(SFXEffectType.KnifeCut);
    }

    public void DisableKnife()
    {
        colliders[0].gameObject.SetActive(false);
    }

    public void EnableBat()
    {
        colliders[1].gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXEffect(SFXEffectType.BatHit);
    }

    public void DisableBat()
    {
        colliders[1].gameObject.SetActive(false);
    }

    public void EnableFlamethrower()
    {
        colliders[2].gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXEffect(SFXEffectType.Flame);
    }

    public void DisableFlamethrower()
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        Flamethrower flamethrower = weapons.FirstOrDefault(w => w.Type == WeaponType.Flamethrower) as Flamethrower;
        if (flamethrower != null)
        {
            flamethrower.UseWeapon(Player.Instance);
        }
        else
        {
            Debug.Log("Flamethrow is not found!");
        }
        colliders[2].gameObject.SetActive(false);
    }

    public void EnableGun()
    {
        raycasts[0].gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXEffect(SFXEffectType.GunShot);
        raycasts[0].RaycastActive();
    }

    public void DisableGun()
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        BulletGun gun = weapons.FirstOrDefault(w => w.Type == WeaponType.Gun) as BulletGun;
        if (gun != null)
        {
            gun.UseWeapon(Player.Instance);
        }
        else
        {
            Debug.Log("Gun is not found!");
        }
        raycasts[0].gameObject.SetActive(false);
    }

    public void EnableRifle()
    {
        WeaponRaycast raycast = raycasts[1];
        raycast.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFXEffect(SFXEffectType.RiffleShot);
        for (int i = 0; i < raycast.amount; i++)
            raycasts[1].RaycastActive();
    }

    public void DisableRiffle()
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        BulletGun riffle = weapons.FirstOrDefault(w => w.Type == WeaponType.Riffle) as BulletGun;
        if (riffle != null)
        {
            riffle.UseWeapon(Player.Instance);
        }
        else
        {
            Debug.Log("Riffle is not found!");
        }
        raycasts[1].gameObject.SetActive(false);
    }
}
