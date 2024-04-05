using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseTail : MonoBehaviour
{
    [SerializeField] Transform tail;
    [SerializeField] float baseSpinSpeed = 100f;
    [SerializeField] float spinFactor = .5f;

    CarMovement carMovement;

    private void Awake()
    {
        carMovement = GetComponent<CarMovement>();
    }

    private void Update()
    {
        if (carMovement.enabled)
        {
            tail.Rotate(0, 0, baseSpinSpeed * carMovement.CurrentSpeed * spinFactor * Time.deltaTime);
        }
    }
}
