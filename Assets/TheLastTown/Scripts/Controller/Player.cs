using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KennMonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance; 
    public PlayerController controller;
    public Inventory inventory;
    public int money;

    private void Awake()
    {
        CreateSingleton();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        controller = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
    }

    private void CreateSingleton()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("Exist Singleton " + instance.name);
    }
}
