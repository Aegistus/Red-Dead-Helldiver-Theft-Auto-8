using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategemBall : MonoBehaviour
{
    public float activationDelay = 3f;
    [HideInInspector] public bool thrown = false;
    
    LineRenderer beam;

    private void Start()
    {
        beam = GetComponent<LineRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown)
        {
            print(collision.gameObject);
            thrown = false;
            StartActivation();
        }
    }

    public void StartActivation()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        beam.enabled = true;
        beam.SetPosition(1, transform.InverseTransformPoint(Vector3.up * 1000f));
        StartCoroutine(ActivationCoroutine());
    }

    IEnumerator ActivationCoroutine()
    {
        yield return new WaitForSeconds(activationDelay);
        Activate();
    }

    protected abstract void Activate();
}
