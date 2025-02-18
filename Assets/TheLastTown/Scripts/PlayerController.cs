using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : KennMonoBehaviour
{
    [SerializeField] protected VariableJoystick joystick;
    [SerializeField] protected Rigidbody2D rig;

    private void Update()
    {
        Vector2 direction = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
        rig.velocity = direction * 10f;
    }
}
