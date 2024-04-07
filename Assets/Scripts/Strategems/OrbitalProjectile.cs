using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalProjectile : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] GameObject explosionPrefab;

    bool collided = false;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            collided = true;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
