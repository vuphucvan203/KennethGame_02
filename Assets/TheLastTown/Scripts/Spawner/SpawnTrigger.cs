using UnityEngine;

public abstract class SpawnTrigger : KennMonoBehaviour
{
    [SerializeField] protected Collider2D coll;
    [SerializeField] protected SpawnRange spawnRange;
    [SerializeField] protected int spawnAmount;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        coll = GetComponent<Collider2D>();
        spawnRange = GetComponentInChildren<SpawnRange>();  
    }

    protected abstract void ActiveSpawner();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActiveSpawner();
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coll.enabled = false;
        }
    }
}
