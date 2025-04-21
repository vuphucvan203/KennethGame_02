using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseStats
{
    protected string statsName;
    [SerializeField] protected int maxValue;
    public int value;

    public BaseStats(string name, int value, int maxValue)
    {
        this.statsName = name;
        this.value = value;
        this.maxValue = maxValue;
    }    


    public void IncreaseStats(int amount)
    {
        value += amount;
    }   
    
    public void DecreaseStats(int amount)
    {
        value -= amount;
        if (value < 0) value = 0; 
    }

    public float Percent => (float) value / maxValue;

    public int Remaining => Mathf.Abs(maxValue - value);

    public bool overValue => value >= maxValue;
}
