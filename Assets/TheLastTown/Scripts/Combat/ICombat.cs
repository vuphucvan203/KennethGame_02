using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    public bool IsAlive();
    public void CalculateDamage(int damage);

    public void TakeDamage(int damage);
}
