using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollier : KennMonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BodyCollider>(out BodyCollider body))
        {
            body.TakeDamage(20);
        }   
    }
}
