using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    SoliderBody,
    SoliderWeapon,
    EnemyBody,
    EnemyWeapon,
}

public class ColliderDefind : KennMonoBehaviour
{
    public ColliderType type;
}

