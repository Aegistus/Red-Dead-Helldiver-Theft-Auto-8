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

    public bool inCombatMode = true;
    Transform cameraTransform;
    CharacterController charController;
    PlayerController playerController;
    Vector3 moveVector;

    Rigidbody rb;
    bool diving = false;
    float divingTime = 3f;
    float diveTimer;
    float diveForce = 400f;
    Vector3 diveForcePosition = new Vector3(0, 1.5f, 0);

    void Awake()
    {
        cameraTransform = FindObjectOfType<CameraController>().transform;
        charController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // diving
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!diving)
            {
                diving = true;
                charController.enabled = false;
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.constraints = 0;
                rb.AddForceAtPosition(transform.forward * diveForce, transform.position + diveForcePosition);
                rb.AddForceAtPosition(transform.up * diveForce * .5f, transform.position + diveForcePosition);
                diveTimer = divingTime;
            }
        }
        if (diving)
        {
            diveTimer -= Time.deltaTime;
            if (diveTimer <= 0)
            {
                diving = false;
                charController.enabled = true;
                rb.useGravity = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        if (!diving)
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
            moveVector.y = 0;
            moveVector = moveVector.normalized;
            //moveVector = transform.TransformDirection(moveVector);
            charController.Move(moveVector * moveSpeed * Time.deltaTime);

            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 20f, groundLayer))
            {
                transform.position = rayHit.point;
            }

            // rotational movement
            if (inCombatMode)
            {
                if (moveVector.sqrMagnitude > 0 && !playerController.Aim)
                {
                    transform.LookAt(transform.position + moveVector);
                    torso.localRotation = Quaternion.identity;
                }
                else
                {
                    transform.rotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    torso.rotation = cameraTransform.rotation;
                    legs.localRotation = Quaternion.identity;
                }
            }
            else
            {
                torso.localRotation = Quaternion.identity;
            }
        }

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
