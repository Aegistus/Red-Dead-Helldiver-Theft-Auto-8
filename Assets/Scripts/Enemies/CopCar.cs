using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopCar : MonoBehaviour
{
    [SerializeField] float updateTargetInterval = 1f;

    NavMeshAgent navAgent;
    Transform target;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            CarMovement car = FindObjectOfType<CarMovement>();
            if (car != null)
            {
                target = car.transform;
            }
        }
        StartCoroutine(UpdateDestination());
    }

    IEnumerator UpdateDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTargetInterval);
            if (target == null)
            {
                CarMovement car = FindObjectOfType<CarMovement>();
                if (car != null)
                {
                    target = car.transform;
                }
            }
            navAgent.SetDestination(target.position);
        }
    }
}
