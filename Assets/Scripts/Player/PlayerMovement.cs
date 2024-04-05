using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : AgentMovement
{
    CharacterController charController;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    Vector3 moveVector;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // lateral movement
        moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector -= Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }
        moveVector = moveVector.normalized;
        moveVector = transform.TransformDirection(moveVector);
        charController.Move(moveVector * moveSpeed * Time.deltaTime);

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 20f, groundLayer))
        {
            transform.position = rayHit.point;
        }

        // rotational movement
        float yRotation = Input.GetAxis("Mouse X");
        yRotation *= turnSpeed;
        transform.Rotate(0, yRotation * Time.deltaTime, 0, Space.Self);
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
