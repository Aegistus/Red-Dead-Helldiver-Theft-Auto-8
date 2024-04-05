using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    private void Update()
    {
        Attack = Input.GetMouseButtonDown(0);
        Reload = Input.GetKeyDown(KeyCode.R);
        SwitchWeapon = Mathf.Abs(Input.mouseScrollDelta.y) > .5f;
    }
}
