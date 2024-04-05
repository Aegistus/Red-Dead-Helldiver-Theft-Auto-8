using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public float spawnRadius = 100;
    public int maxPedestrianCount = 20;

    int currentPedestrianCount = 0;

    void Awake()
    {
        PedestrianDeath.OnPedestrianDeath += IncrementPedestrianCount;
    }

    void Start()
    {
        StartCoroutine(RespawnPedestrians());
    }

    IEnumerator RespawnPedestrians()
    {
        //while (true)
        //{
            int countToSpawn = maxPedestrianCount - currentPedestrianCount;
            for (int i = 0; i < countToSpawn; i++)
            {
                SpawnPedestrian();
            }
            yield return new WaitForSeconds(30f);
        //}
    }

    void SpawnPedestrian()
    {
        Vector3 position = (Random.insideUnitCircle * spawnRadius);
        position.z = position.y;
        position += transform.position;
        position.y = 2;
        Instantiate(pedestrianPrefab, position, Quaternion.identity);
    }

    void IncrementPedestrianCount()
    {
        currentPedestrianCount++;
    }

    void DecrementPedestrianCount()
    {
        currentPedestrianCount--;
    }

    void OnDestroy()
    {
        PedestrianDeath.OnPedestrianDeath -= DecrementPedestrianCount;
    }
}
