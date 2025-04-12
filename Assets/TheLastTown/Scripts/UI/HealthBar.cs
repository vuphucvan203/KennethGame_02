using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : KennMonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected SpawnerSample spawner;
    [SerializeField] protected Slider healthBarSlider;
    [SerializeField] protected HealthBarType type;
    public HealthBarType Type => type;
    protected Vector3 offset;

    private void Update()
    {
        SetSliderValue(character.Health.Percent);
        FollowCharacter();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        spawner = GetComponentInParent<SpawnerSample>();
        healthBarSlider = GetComponent<Slider>();
    }

    public void SetSliderValue(float value)
    {
        if(!character) return;
        healthBarSlider.value = value;
        if (value == 0) spawner.DespawnObject(this.transform);
    }    

    public void SetCharacter(Character _character)
    {
        character = _character;
        float yOffset = 0;
        switch(type)
        {
            case HealthBarType.S_EnemyHB:
                yOffset = 1f;
                break;
            case HealthBarType.M_EnemyHB:
                yOffset = 2f;
                break;
            case HealthBarType.XL_EnemyHB:
                yOffset = 3f;
                break;
            case HealthBarType.XXL_EnemyHB:
                yOffset = 4f;
                break;
            case HealthBarType.XXXL_EnemyHB:
                yOffset = 6f;
                break;
        }    
        offset = new Vector3(0, yOffset, 0);
        transform.position = _character.transform.position + offset;
    }

    public void FollowCharacter()
    {
        if (!character) return;
        transform.position = Vector3.MoveTowards(transform.position, character.transform.position + offset, Time.deltaTime * 5f);
    }    
}
public enum HealthBarType
{
    PlayerHB,
    S_EnemyHB,
    M_EnemyHB,
    XL_EnemyHB,
    XXL_EnemyHB,
    XXXL_EnemyHB
}

public class HealthBarFinder : MonoBehaviour
{
    protected static HealthBar[] healthBars;

    public static HealthBar FindByType(HealthBarType type)
    {
        healthBars = FindObjectsOfType<HealthBar>();
        foreach(HealthBar bar in healthBars)
        {
            if (type == bar.Type) return bar;
        }
        return null;
    }
} 
    
