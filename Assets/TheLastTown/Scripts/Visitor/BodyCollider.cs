using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BodyCollider : KennMonoBehaviour
{
    [SerializeField] protected Character character;
    public ColliderType colliderDefind;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        character = GetComponentInParent<Character>();
    }

    public void TakeDamage(int damage)
    {
        int damageReduce = damage - character.ApplyDefenseStats(damage);
        character.Health.DecreaseStats(damageReduce);
    }
}
