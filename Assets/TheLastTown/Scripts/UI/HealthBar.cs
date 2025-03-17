using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : KennMonoBehaviour
{
    [SerializeField] protected Character character;
    [SerializeField] protected Slider healthBarSlider;
    [SerializeField] protected HealthBarType type;
    public HealthBarType Type => type;

    private void Update()
    {
        SetSliderValue(character.Health.Percent);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        healthBarSlider = GetComponent<Slider>();
    }

    public void SetSliderValue(float value)
    {
        if(!character) return;
        healthBarSlider.value = value;
    }    

    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    public void FollowCharacter(Character _character)
    {
        transform.Translate(_character.transform.position * 5f);
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
    
