using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [Header("Camera target position")]
      public Transform target;

    [Header("Camera Aim Settings")]
      [SerializeField] float verticalSensitivity;
      [SerializeField] float horizontalSensitivity;
      [SerializeField] float maxLookUpAngle;
      [SerializeField] float minLookUpAngle;
      [SerializeField] float fov;
    
      [Header("ADS")]
      [SerializeField] float adsVerticalSensitivity;
      [SerializeField] float adsHorizontalSensitivity;
      [SerializeField] ADSType aimingmode;
      [SerializeField] float adsFOV;
      [SerializeField] float adsSpeed;

      private Vector2 _mouseInput;
      private bool aimingDownSights = false;
      private float xSensitivity;
      private float ySensitivity;
      Camera main;
      public FirstPersonInputmanager playerInputController;
      public Interact _interact;

      public string controlScheme;

      [SerializeField] float rotX = 0, rotY = 0;
      [SerializeField] bool canMove = true;

      public enum ADSType
    {
        HoldToAim,
        ClickToAim
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        main = GetComponent<Camera>();

        verticalSensitivity = verticalSensitivity / 100;
        horizontalSensitivity = horizontalSensitivity / 100;

        _interact = GetComponent<Interact>();
        
    }

    private void Update()
    {
        DisableADS();
        _mouseInput = playerInputController.inputActions.FirstPersonInputMap.Look.ReadValue<Vector2>();
        if (canMove)
        {
            rotX += _mouseInput.x * xSensitivity;
            rotY += _mouseInput.y * ySensitivity;
            rotY = Mathf.Clamp(rotY, minLookUpAngle, maxLookUpAngle);
        }
        transform.localRotation = Quaternion.Euler(-rotY, rotX, 0f);
        
        transform.position = target.position;
    }
    
    private void ADS()
    {
        switch (aimingmode)
        {
            case ADSType.ClickToAim:
                if(playerInputController.inputActions.FirstPersonInputMap.ADS.triggered)
                aimingDownSights = !aimingDownSights;
                break;

            case ADSType.HoldToAim:
                playerInputController.inputActions.FirstPersonInputMap.ADS.performed += aim => aimingDownSights = true;
                playerInputController.inputActions.FirstPersonInputMap.ADS.canceled += aim => aimingDownSights = false;
                break;
        }

        if (aimingDownSights)
        {
            main.fieldOfView = Mathf.Lerp(main.fieldOfView, adsFOV, adsSpeed);
            xSensitivity = adsHorizontalSensitivity / 10;
            ySensitivity = adsVerticalSensitivity / 10;
        }
        else
        {
            main.fieldOfView = Mathf.Lerp(main.fieldOfView, fov, adsSpeed);
            xSensitivity = horizontalSensitivity;
            ySensitivity = verticalSensitivity;
        }
    }

    private void DisableADS()
    {
        if (_interact.holdingObject == true)
        {
            main.fieldOfView = Mathf.Lerp(main.fieldOfView, fov, adsSpeed);
            xSensitivity = horizontalSensitivity;
            ySensitivity = verticalSensitivity; 
        }
        else
        {
            ADS();
        }
    }
}
