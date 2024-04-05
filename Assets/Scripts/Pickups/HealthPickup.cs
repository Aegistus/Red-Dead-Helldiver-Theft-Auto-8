using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float health = 10f;
    public float radius = 2f;
    public LayerMask playerMask;

    Collider[] hits = new Collider[10];
    void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, hits, playerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] != null)
            {
                PlayerHealth health = hits[i].GetComponentInParent<PlayerHealth>();
                if (health)
                {
                    health.Heal(this.health);
                    Destroy(gameObject);
                }
            }
        }
    }
}
