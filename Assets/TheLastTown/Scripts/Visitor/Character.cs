using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : KennMonoBehaviour
{
    protected string characterName;

    public Character(string name)
    {
        characterName = name;
    }    
}
