using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    public bool isExcute;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isExcute = true;
        }
        else
        {
            isExcute = false;
        }
    }

    public void OnClick()
    {
        isExcute = true;
    }
}
