using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFront : KennMonoBehaviour
{
    [SerializeField] protected bool hasObstacle;
    public bool HasObstacle => hasObstacle;
    protected Collider2D coll;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject != gameObject && !collision.CompareTag("Player") && !collision.CompareTag("Trigger"))
        {
            hasObstacle = true;
        }    
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject && !collision.CompareTag("Player") && !collision.CompareTag("Trigger"))
        {
            hasObstacle = false;
        }
    }
}
