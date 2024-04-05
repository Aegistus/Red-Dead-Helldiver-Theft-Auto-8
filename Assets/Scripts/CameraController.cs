using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform targetTransform;
    public Camera frontCamera;
    public Camera backCamera;
	public float smoothSpeed = .5f;
    public float mouseSensitivity = 500f;
    public float cameraVerticalMaxAngle = 80f;
    public float cameraVerticalMinAngle = -80f;

    CameraShake camShake;
    AgentEquipment equipment;

    void Start()
    {
        equipment = targetTransform.GetComponent<AgentEquipment>();
        equipment.OnWeaponChange += Equipment_OnWeaponChange; ;
        camShake = GetComponentInChildren<CameraShake>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Equipment_OnWeaponChange()
    {
        equipment.CurrentWeaponAttack.OnRecoil.AddListener(ScreenShake);
    }

    void LateUpdate()
	{
        float verticalMovement = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float xRotation = transform.eulerAngles.x + verticalMovement;
        float yRotation = transform.eulerAngles.y + horizontalMovement;
        
        if (xRotation > 180)
        {
            xRotation -= 360;
        }
        xRotation = Mathf.Clamp(xRotation, cameraVerticalMinAngle, cameraVerticalMaxAngle);

        transform.position = Vector3.Lerp(transform.position, targetTransform.position, smoothSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(xRotation, yRotation, transform.eulerAngles.z);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            backCamera.gameObject.SetActive(true);
            frontCamera.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            backCamera.gameObject.SetActive(false);
            frontCamera.gameObject.SetActive(true);
        }
    }

    void ScreenShake()
    {
        camShake.StartShake(equipment.CurrentWeaponAttack.camShakeProperties);
    }
}
