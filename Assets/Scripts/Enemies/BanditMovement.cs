using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditMovement : AgentMovement
{
    [SerializeField] float detectionRadius = 30f;
    [SerializeField] float standDistance = 10f;

    public bool InAttackRange { get; private set; }

    NavMeshAgent navAgent;
    Transform playerTransform;

    bool hunting = false;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerHealth>().transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        InAttackRange = false;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= detectionRadius)
        {
            if (distance > standDistance)
            {
                hunting = true;
                navAgent.SetDestination(playerTransform.position);
            }
            else
            {
                navAgent.SetDestination(transform.position);
                InAttackRange = true;
                transform.LookAt(playerTransform);
            }
        }
    }
}
