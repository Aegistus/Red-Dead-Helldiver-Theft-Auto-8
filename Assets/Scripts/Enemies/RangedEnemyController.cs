using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : AgentController
{
    [SerializeField] float attackInterval = .5f;

    BanditMovement movement;
    AgentEquipment equipment;
    bool canAttack = true;

    private void Awake()
    {
        movement = GetComponent<BanditMovement>();
        equipment = GetComponent<AgentEquipment>();
    }

    private void Update()
    {
        StartAttack = false;
        Reload = false;
        if (movement.InAttackRange && canAttack)
        {
            StartAttack = true;
            StartCoroutine(AttackDelayCoroutine());
        }
        if (equipment.CurrentWeaponAmmunition.CurrentLoadedAmmo == 0)
        {
            equipment.CurrentWeaponAmmunition.TryReload();
        }
    }

    IEnumerator AttackDelayCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }
}
