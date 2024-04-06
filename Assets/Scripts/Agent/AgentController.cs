using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    public bool StartAttack { get; protected set; } = false;
    public bool DuringAttack { get; protected set; } = false;
    public bool EndAttack { get; protected set; } = false;
    public bool Reload { get; protected set; } = false;
    public bool SwitchWeapon { get; protected set; } = false;
    public bool Aim { get; protected set; } = false;
}
