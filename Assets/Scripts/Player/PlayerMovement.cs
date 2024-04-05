using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : AgentMovement
{
    [SerializeField] Transform torso;
    [SerializeField] Transform legs;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    Transform cameraTransform;
    CharacterController charController;
    Vector3 moveVector;

    void Awake()
    {
        cameraTransform = FindObjectOfType<CameraController>().transform;
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // lateral movement
        moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVector += cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector -= cameraTransform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += cameraTransform.right;
        }
        moveVector = moveVector.normalized;
        //moveVector = transform.TransformDirection(moveVector);
        charController.Move(moveVector * moveSpeed * Time.deltaTime);

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 20f, groundLayer))
        {
            transform.position = rayHit.point;
        }

        // rotational movement
        torso.rotation = cameraTransform.rotation;
        legs.rotation = cameraTransform.rotation;
        legs.eulerAngles = new Vector3(0, legs.eulerAngles.y, 0);

        // fix falling through map
        if (transform.position.y <= -100)
        {
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        }
    }

    void OnDisable()
    {
        moveVector = Vector3.zero;
        charController.enabled = false;
    }

    void OnEnable()
    {
        moveVector = Vector3.zero;
        charController.enabled = true;
    }
}
