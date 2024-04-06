using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    private void Update()
    {
        StartAttack = Input.GetMouseButtonDown(0);
        EndAttack = Input.GetMouseButtonUp(0);
        Reload = Input.GetKeyDown(KeyCode.R);
        SwitchWeapon = Mathf.Abs(Input.mouseScrollDelta.y) > .5f;
        Aim = Input.GetMouseButton(1);
    }
}
