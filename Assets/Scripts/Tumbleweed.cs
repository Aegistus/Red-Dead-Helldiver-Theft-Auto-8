using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    [SerializeField] float moveForce = 100f;
    [SerializeField] float minDelay = 5f;
    [SerializeField] float maxDelay = 15f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector3 direction = new Vector3(Random.value, Random.value, Random.value);
            rb.AddForce(direction * moveForce);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}
