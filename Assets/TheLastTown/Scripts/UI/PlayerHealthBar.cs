using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : KennMonoBehaviour
{
    [SerializeField] protected Slider healthBarSlider;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        healthBarSlider = GetComponent<Slider>();
    }

    public void SetSliderValue(float value)
    {
        healthBarSlider.value = value;
    }    
}

    
