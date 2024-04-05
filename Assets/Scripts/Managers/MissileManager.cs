using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public GameObject missilePrefab;
    public float spawnRadius = 100f;
    public float spawnHeight = 50f;
    public float fiveStarMissileSpeed = 30f;

    int currentMissiles = 0;
    int targetNumOfMissiles;
    int maxMissiles = 100;
    float spawnInterval = 20f;
    int[] missilesPerWantedLevel = { 0, 10, 20, 40, 80, 100 };

    void Start()
    {
        Missile.OnMissileDestroy += DecrementMissiles;
        GameManager.Instance.OnWantedLevelChange += UpdateMissilesWithWantedLevel;
        StartCoroutine(RespawnMissilesPeriodically());
    }

    void DecrementMissiles()
    {
        currentMissiles--;
    }

    public void SpawnMissile()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition.z = spawnPosition.y;
        spawnPosition.y = spawnHeight;
        Missile missile = Instantiate(missilePrefab, spawnPosition, Quaternion.identity).GetComponent<Missile>();
        if (GameManager.Instance.CurrentWantedLevel == 5)
        {
            missile.speed = fiveStarMissileSpeed;
        }
        currentMissiles++;
    }

    public void UpdateMissilesWithWantedLevel(int wantedLevel)
    {
        print("TEST");
        targetNumOfMissiles = missilesPerWantedLevel[wantedLevel];
    }


    IEnumerator RespawnMissilesPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int neededMissiles = targetNumOfMissiles - currentMissiles;
            for (int i = 0; i < neededMissiles; i++)
            {
                SpawnMissile();
            }
        }
    }

    void OnDestroy()
    {
        Missile.OnMissileDestroy -= DecrementMissiles;
        GameManager.Instance.OnWantedLevelChange -= UpdateMissilesWithWantedLevel;
    }
}
