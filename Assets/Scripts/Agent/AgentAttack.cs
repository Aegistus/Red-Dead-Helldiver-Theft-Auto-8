using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    AgentController controller;
    AgentEquipment equipment;

    private void Start()
    {
        controller = GetComponent<AgentController>();
        equipment = GetComponent<AgentEquipment>();
    }

    private void Update()
    {
        if (controller.SwitchWeapon)
        {
            equipment.TrySwitchWeapon();
            return;
        }
        if (controller.Attack && equipment.CurrentWeapon != null)
        {
            equipment.CurrentWeaponAttack.BeginAttack();
        }
        if (controller.Reload)
        {
            equipment.CurrentWeaponAmmunition.TryReload();
        }
    }
}
