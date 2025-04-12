using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : KennMonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected ColliderType target;
    public int amount;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        character = GetComponentInParent<Character>();
    }

    public void RaycastActive()
    {
        Ray2D ray = new Ray2D(transform.position, -transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        RaycastHit2D[] hits  = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
        foreach (var hit in hits)
        {
            if (hit.collider == null) return;
            if (hit.collider.CompareTag("Player")) continue;
            if (hit.collider.TryGetComponent<BodyCollider>(out BodyCollider body))
            {
                if (body.colliderDefind != target) return;
                character.Accept(new CalculateDamageVisitor());
                body.TakeDamage(character.currentDamage);
                break;
            }
        }
    }    
}
