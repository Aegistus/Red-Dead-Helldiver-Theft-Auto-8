using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using System;

public class PedestrianDeath : MonoBehaviour
{
    public static event Action OnPedestrianDeath;

    [SerializeField] int carLayer;
    [SerializeField] float despawnTime;
    Rigidbody rb;
    PedestrianMovement movement;
    NavMeshAgent navAgent;
    float deathForce = 20f;
    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PedestrianMovement>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == carLayer && !isDead)
        {
            CarMovement car = collision.gameObject.GetComponentInParent<CarMovement>();
            if (car.enabled)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        rb.isKinematic = false;
        movement.enabled = false;
        navAgent.enabled = false;
        Vector3 forceDirection = Random.onUnitSphere;
        rb.AddForce(forceDirection * deathForce, ForceMode.Impulse);
        GameManager.Instance.DeadPedestrian();
        StartCoroutine(Despawn());
        isDead = true;
        OnPedestrianDeath?.Invoke();
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
