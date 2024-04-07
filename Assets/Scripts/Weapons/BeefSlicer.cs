using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefSlicer : WeaponAttack
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] DamageSource damageSource;
    [SerializeField] Transform[] sawCenterPoints;
    [SerializeField] float sawRadius = 1f;

    Collider[] hits = new Collider[10];

    private void Start()
    {
        Source = damageSource;
    }

    public override void BeginAttack()
    {
        
    }

    public override void DuringAttack()
    {
        for (int i = 0; i < sawCenterPoints.Length; i++)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(sawCenterPoints[i].position, sawRadius, hits, playerLayer);
            for (int j = 0; j < hitCount; j++)
            {
                if (hits[j].TryGetComponent(out PlayerHealth health))
                {
                    health.Damage(Random.Range(damageMin, damageMax) * Time.deltaTime);
                }
            }
        }
    }

    public override void EndAttack()
    {
        
    }
}
