using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float damage = 100f;
    [SerializeField] float radius = 10f;
    [SerializeField] LayerMask targetLayers;
    [SerializeField] float explosionForce = 100f;
    [SerializeField] float lifeTime = 10f;

    void Start()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetLayers);
        for (int i = 0; i < hits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, hits[i].transform.position);
            float adjustedDamage = damage * ((radius - distance) / radius);
            if (hits[i].TryGetComponent(out AgentHealth health))
            {
                health.Damage(adjustedDamage, DamageSource.Environment);
            }
            if (hits[i].TryGetComponent(out PlayerHealth pHealth))
            {
                pHealth.Damage(adjustedDamage);
            }
        }
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
