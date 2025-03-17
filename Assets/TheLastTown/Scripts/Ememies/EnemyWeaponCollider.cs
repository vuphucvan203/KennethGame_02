using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : KennMonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    public ColliderType colliderType;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {

    }
}