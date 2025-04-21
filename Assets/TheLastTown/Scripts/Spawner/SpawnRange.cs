using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnRange : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D range;
    [SerializeField] protected List<Vector2> spawnPositions;
    public float minDistance = 5f;
    public float minRadiusValid = 1f;
    public LayerMask layerMask;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadRange();
    }

    protected void LoadRange()
    {
        range = GetComponent<CircleCollider2D>();
    }

    public List<Vector2> GetRandomSpawnPosition(int amount)
    {
        int attempts = 0;
        int maxAttempts = amount * 10;

        
        while (spawnPositions.Count < amount)
        {
            attempts++;
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * range.radius;
            float x = transform.position.x + distance * Mathf.Cos(angle);
            float y = transform.position.y + distance * Mathf.Sin(angle);
            Vector2 newPos = new Vector2(x, y);

            if (spawnPositions.Any(p => Vector2.Distance(p, newPos) < minDistance)) continue;

            if (Physics2D.OverlapCircle(newPos, minRadiusValid, layerMask)) continue;

            spawnPositions.Add(newPos);

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Over limit of attempts");
                break;
            }
        }
        return spawnPositions;
    }
}
