using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteraction : MonoBehaviour
{
    public Transform saddle;

    int openDoorSoundID;

    void Start()
    {
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.Instance.PlaySoundAtPosition(openDoorSoundID, transform.position);
            GameManager.Instance.PlayerExitCar(GetComponent<CarMovement>());
        }
    }
}
