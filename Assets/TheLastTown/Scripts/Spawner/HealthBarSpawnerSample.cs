using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpawnerSample : SpawnerSample
{
    [SerializeField] protected HealthBarType healthBarType;
    public HealthBarType HealthBarType { get => healthBarType; set => healthBarType = value; }

    public Transform Spawn(Character character)
    {
        Transform newHealthBar = GetFromPoolingObject(spawnObjects); 
        HealthBar healthBar = newHealthBar.GetComponent<HealthBar>();
        healthBar.SetCharacter(character);
        newHealthBar.SetParent(holder.transform);
        newHealthBar.gameObject.SetActive(true);
        newHealthBar.transform.SetParent(holder.transform);
        spawnCount++;
        HealthBarSpawnSystem.Instance.isSpawn = true;
        return newHealthBar;
    }
}
    