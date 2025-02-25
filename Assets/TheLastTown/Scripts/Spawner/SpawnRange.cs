using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnRange : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D range;
    [SerializeField] protected List<Vector2> spawnPositions;
    public float minDistance = 5f;

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
        int count = 0;
        int attempts = 0;
        int maxAttempts = 100;

        
        while (count < amount)
        {
            attempts++;
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * range.radius;
            float x = transform.position.x + distance * Mathf.Cos(angle);
            float y = transform.position.y + distance * Mathf.Sin(angle);
            Vector2 newPos = new Vector2(x, y);

            bool isValid = true;
            foreach(var pos in spawnPositions)
            {
                if(Mathf.Abs(Vector2.Distance(pos, newPos)) < minDistance)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                spawnPositions.Add(newPos);
                count++;
            }

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Over limit of attempts");
                break;
            }
        }
        return spawnPositions;
    }
}
