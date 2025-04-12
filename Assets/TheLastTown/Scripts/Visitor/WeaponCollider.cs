using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponCollider : KennMonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Character character;
    public ColliderType colliderTarget;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        character = GetComponentInParent<Character>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<BodyCollider>(out BodyCollider body))
        {
            if (body.colliderDefind != colliderTarget) return;
            character.Accept(new CalculateDamageVisitor());
            body.TakeDamage(character.currentDamage);
        }
    }
}
