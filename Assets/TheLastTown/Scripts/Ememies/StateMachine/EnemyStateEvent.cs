using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateEvent : KennMonoBehaviour
{
    [SerializeField] protected EnemyAI enemyAI;
    [SerializeField] protected WeaponCollider[] colliers;


    private void Awake()
    {
        base.LoadComponent();
        LoadColliders();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    protected void LoadColliders()
    {
        colliers = GetComponentsInChildren<WeaponCollider>();
        foreach (WeaponCollider collider in colliers) 
        {
            collider.gameObject.SetActive(false);
        }
    }    

    public void StartCooldown()
    {
        enemyAI.startCooldown = true;
    }    

    public void DeathHandle()
    {
        enemyAI.Enemy.Spawner.DespawnObject(transform.parent);
    }    
}
