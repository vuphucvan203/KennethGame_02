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
        int amount = enemyAI.Enemy.Price;
        List<Vector2> positions = GetPostions(amount);
        ItemSpawnSystem.Instance.SpawnItems(ItemType.Money, amount, positions);
        Player.Instance.controller.Soldier.Experience.IncreaseStats(amount * 5);
    }    

    protected List<Vector2> GetPostions(int amount)
    {
        List<Vector2> positions = new();

        while (positions.Count < amount)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * 1f;
            float x = transform.position.x + distance * Mathf.Cos(angle);
            float y = transform.position.y + distance * Mathf.Sin(angle);
            Vector2 newPos = new Vector2(x, y);
            positions.Add(newPos);
        }

        return positions;
    }    
}
