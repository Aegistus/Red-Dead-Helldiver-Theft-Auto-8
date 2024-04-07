using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropStratagem : StrategemBall
{
    [SerializeField] float spawnHeight = 200f;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject dropPodPrefab;

    protected override void Activate()
    {
        GameObject dropPod = Instantiate(dropPodPrefab, transform.position + Vector3.up * spawnHeight, Quaternion.identity);
        DropPod pod = dropPod.GetComponent<DropPod>();
        pod.dropPrefab = weaponPrefab;
        Destroy(gameObject);
    }
}
