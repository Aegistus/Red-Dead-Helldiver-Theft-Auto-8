using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CarMovement : MonoBehaviour
{
	public LayerMask groundLayer;
    public float maxSpeed = 20f;
	public float turnSpeed = 20f;
	[Range(0f, 1f)]
	public float acceleration = .5f;
	[Range(0f, 1f)]
	public float brakeDeceleration = .5f;
	[Range(0f, 1f)]
	public float coastDeceleration = .1f;
	public float inAirDeceleration = 3f;

	public float CurrentSpeed => currentSpeed;

	float currentSpeed = 0f;
	int runningSoundID;
	int impactSoundID;
	int carStartSoundID;
	Rigidbody rb;
	bool obstructed = false;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void Start()
	{
		runningSoundID = SoundManager.Instance.GetSoundID("Car_Running");
		impactSoundID = SoundManager.Instance.GetSoundID("Car_Impact");
		carStartSoundID = SoundManager.Instance.GetSoundID("Car_Start");

	}

	void OnEnable()
	{
		SoundManager.Instance.PlayGlobalFadeIn(runningSoundID, 1f);
		SoundManager.Instance.PlaySoundAtPosition(carStartSoundID, transform.position);
	}

	void Update()
	{
		if (!IsGrounded())
		{
			transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
			currentSpeed -= (inAirDeceleration * Time.deltaTime);
			currentSpeed = Mathf.Clamp(currentSpeed, 0, float.MaxValue);
            if (transform.position.y <= -100)
            {
                transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            }
            //print(currentSpeed);
            return;
		}
		float forwardInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		if (forwardInput > 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
		}
		else if (forwardInput < 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed, brakeDeceleration * Time.deltaTime);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, 0f, coastDeceleration * Time.deltaTime);
		}
		if (obstructed)
		{
			currentSpeed = Mathf.Clamp(currentSpeed, float.NegativeInfinity, 0);
		}
		currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
		transform.Rotate(Vector3.up * horizontalInput * turnSpeed * currentSpeed * Time.deltaTime, Space.Self);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			obstructed = true;
			SoundManager.Instance.PlaySoundAtPosition(impactSoundID, transform.position);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			obstructed = false;
		}
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(transform.position + (Vector3.up * .5f), -transform.up, 3f, groundLayer);
	}
}
