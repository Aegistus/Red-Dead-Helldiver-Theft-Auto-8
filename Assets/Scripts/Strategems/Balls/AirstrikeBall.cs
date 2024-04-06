using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeBall : StrategemBall
{
    public GameObject missilePrefab;
    public float missileSpawnHeight = 100f;

    protected override void Activate()
    {
        Transform missileTransform = Instantiate(missilePrefab, transform.position + Vector3.up * missileSpawnHeight, Quaternion.identity).transform;
        missileTransform.LookAt(missileTransform.position + Vector3.down);
        Destroy(gameObject);
    }
}
