using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimReticleActual : MonoBehaviour
{
    [SerializeField] RectTransform reticle;
    [SerializeField] LayerMask collidableMask;

    AgentEquipment playerEquipment;
    RaycastHit rayHit;

    private void Start()
    {
        playerEquipment = FindObjectOfType<PlayerController>().GetComponent<AgentEquipment>();
    }

    private void Update()
    {
        if (playerEquipment.HasWeaponEquipped)
        {
            Transform projectileSpawnPoint = ((RangedWeaponAttack)playerEquipment.CurrentWeaponAttack).ProjectileSpawnPoint;
            if (Physics.Raycast(projectileSpawnPoint.position, projectileSpawnPoint.forward, out rayHit, 100f, collidableMask))
            {
                reticle.position = Camera.main.WorldToScreenPoint(rayHit.point);
            }
        }
    }
}
