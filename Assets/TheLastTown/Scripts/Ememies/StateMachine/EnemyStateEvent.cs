using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateEvent : KennMonoBehaviour
{
    [SerializeField] protected EnemyAI enemyAI;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void StartCooldown()
    {
        enemyAI.startCooldown = true;
    }    
}
