using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    public bool Attack { get; protected set; } = false;
    public bool Reload { get; protected set; } = false;
    public bool SwitchWeapon { get; protected set; } = false;
}
