using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<bool> OnInteractStateChange;

    public LayerMask interactableLayers;
    public float interactionDistance = 1f;
    int openDoorSoundID;

    AgentEquipment agentEquipment;
    Vector3 halfExtents = new Vector3(1, 1, 2);
    Cow currentlyMilking;

    void Start()
    {
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
        agentEquipment = GetComponent<AgentEquipment>();
    }

    RaycastHit rayHit;
    void Update()
    {
        if (Physics.BoxCast(transform.position + Vector3.up, halfExtents, transform.forward, out rayHit, Quaternion.identity, interactionDistance, interactableLayers))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CarMovement car = rayHit.collider.GetComponentInParent<CarMovement>();
                WeaponAttack weapon = rayHit.collider.GetComponentInParent<WeaponAttack>();
                currentlyMilking = rayHit.collider.GetComponentInParent<Cow>();
                if (car != null)
                {
                    SoundManager.Instance.PlaySoundAtPosition(openDoorSoundID, transform.position);
                    GameManager.Instance.PlayerEnterCar(car);
                }
                else if (weapon != null)
                {
                    agentEquipment.PickupWeapon(weapon.gameObject);
                }
                else if (currentlyMilking != null)
                {
                    currentlyMilking.Milk();
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (currentlyMilking != null)
                {
                    currentlyMilking.Milk();
                }
            }
            OnInteractStateChange?.Invoke(true);
            //print("In interaction range");
        }
        else
        {
            OnInteractStateChange?.Invoke(false);
            currentlyMilking = null;
        }
    }
}
