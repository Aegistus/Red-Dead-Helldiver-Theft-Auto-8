using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    [SerializeField] float dropSpeed = 100f;

    public GameObject dropPrefab;

    float spawnDelay = 2f;
    bool moving = true;

    private void Update()
    {
        if (moving)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (moving)
        {
            moving = false;
            StartCoroutine(SpawnDrop());
        }
    }

    IEnumerator SpawnDrop()
    {
        yield return new WaitForSeconds(spawnDelay);
        GameObject drop = Instantiate(dropPrefab, transform.position, transform.rotation);
        if (drop.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        Destroy(gameObject);
    }
}
