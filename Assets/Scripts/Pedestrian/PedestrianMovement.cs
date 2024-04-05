using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianMovement : MonoBehaviour
{
    [SerializeField] float wanderRange = 20f;
    [SerializeField] float waitTimer = 5f;

    NavMeshAgent navAgent;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (enabled)
        {
            Vector2 destination = Random.insideUnitCircle * wanderRange;
            navAgent.SetDestination(new Vector3(destination.x, 0, destination.y));
            yield return new WaitForSeconds(waitTimer);
        }
    }

}
