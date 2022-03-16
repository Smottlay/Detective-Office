using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Analyze : MonoBehaviour
{
    public FirstPersonInputmanager _playerInputController;
    public Interact _interact;
    public CameraController _cameraController;
    public PlayerMovementController _playerMovementController;
    
    public Transform inspectTarget;
    public float rotationSpeed;
    
    private bool dynaSwitch;
    
    private void Update()
    {
        Inspect();
    }

    public void Inspect()
    {
        if (_playerInputController.inputActions.FirstPersonInputMap.ADS.triggered && _interact.holdingObject)
        {
            dynaSwitch = !dynaSwitch;
            if (dynaSwitch)
            {
                _interact.heldObject.transform.position = inspectTarget.position;
                _interact.heldObject.transform.parent = GameObject.Find("InspectPosition").transform;
                _interact.heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _interact.heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                
                _playerMovementController.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _playerMovementController.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                
                _playerMovementController.GetComponent<PlayerMovementController>().enabled = false;
                _cameraController.GetComponent<CameraController>().enabled = false;
                _interact.pickupDistance = 0.001f;

                Vector2 mousePosition = Mouse.current.position.ReadValue();
                float xAxis = mousePosition.x;
                float yAxis = mousePosition.y;

                _interact.heldObject.transform.Rotate(xAxis*rotationSpeed*Time.deltaTime,yAxis*rotationSpeed*Time.deltaTime,0,Space.World);
                
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            else if (!dynaSwitch)
            {
                _interact.heldObject.transform.position = _interact.objTarget.position;
                _interact.heldObject.transform.parent = GameObject.Find("HoldPosition").transform;
                _interact.heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _interact.heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                
                _playerMovementController.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _playerMovementController.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                
                _playerMovementController.GetComponent<PlayerMovementController>().enabled = true;
                _cameraController.GetComponent<CameraController>().enabled = true;
                _interact.pickupDistance = 1f;
                
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
        else if (!_interact.holdingObject)
        {
            _playerMovementController.GetComponent<PlayerMovementController>().enabled = true;
            _cameraController.GetComponent<CameraController>().enabled = true;
            _interact.pickupDistance = 1f;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
}
