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
        raycasts[1].gameObject.SetActive(false);
    }
}
