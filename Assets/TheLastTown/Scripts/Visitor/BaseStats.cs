using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseStats
{
    protected string statsName;
    [SerializeField] protected int maxValue;
    [SerializeField] protected int value;
    public int Value => value;

    public BaseStats(string name, int val)
    {
        statsName = name;
        value = maxValue = val;
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
}
