using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Vector3 movement;
    [SerializeField] bool relativeToSelf = true;

    private void Update()
    {
        transform.Translate(movement * Time.deltaTime, relativeToSelf ? Space.Self : Space.World);
    }
}
