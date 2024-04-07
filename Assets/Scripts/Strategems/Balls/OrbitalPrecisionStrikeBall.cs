using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalPrecisionStrikeBall : StrategemBall
{
    [SerializeField] float spawnElevation = 500f;
    [SerializeField] GameObject projectilePrefab;

    protected override void Activate()
    {
        Instantiate(projectilePrefab, transform.position + Vector3.up * spawnElevation, Quaternion.identity);
        Destroy(gameObject);
    }
}
