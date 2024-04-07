using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AgentController
{
    BanditMovement movement;
    AgentEquipment equipment;

    void Start()
    {
        movement = GetComponent<BanditMovement>();
        equipment = GetComponentInChildren<AgentEquipment>();
    }

    private void Update()
    {
        if (movement.Hunting)
        {
            equipment.CurrentWeaponAttack.DuringAttack();
        }
    }

}
