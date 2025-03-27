using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KennMonoBehaviour
{
    public PlayerController controller;
    public Inventory inventory;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        controller = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
    }
}
