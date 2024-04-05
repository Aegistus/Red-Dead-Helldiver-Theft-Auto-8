using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] float delay = 10f;

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(delay);
    }
}
