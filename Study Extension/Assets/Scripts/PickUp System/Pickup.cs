using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class Pickup : MonoBehaviour
{
    public FirstPersonInputmanager _playerInputController;

    public Transform objTarget;
    public Transform dropTarget;

    public bool holdingObject;
    private GameObject heldObject;
    [SerializeField] float rayDistance;
    
    
    private void Start()
    {
        Physics.IgnoreLayerCollision(6,3);
        holdingObject = false;
    }

    public void Update()
    {
        HandleAllPickupFunctions();
    }
    
    public void HandleAllPickupFunctions()
    {
        PickupSystem();
        DropObject();
    }
    private void PickupSystem()
    {
        RaycastHit hit;
        Ray raycast = new Ray(transform.position, transform.forward);
        
        if (_playerInputController.inputActions.FirstPersonInputMap.Fire.triggered)
        {
            if (Physics.Raycast(raycast, out hit, rayDistance))
            {
                print("Found an object - distance: " + hit.distance);
                //Debug.DrawLine(raycast.origin, hit.point, Color.blue, 200);

                if (hit.transform.CompareTag("PickObject"))
                {
                    #region GrabObject
                    hit.rigidbody.useGravity = false;
                    hit.rigidbody.velocity = Vector3.zero;
                    hit.rigidbody.angularVelocity = Vector3.zero;
                    hit.transform.position = objTarget.position;
                    hit.transform.parent = GameObject.Find("HoldPosition").transform;
                    hit.transform.rotation = transform.rotation;
                    heldObject = hit.transform.gameObject;

                    holdingObject = true;
                    #endregion
                }
            }
        }
    }

    private void DropObject()
    {
        if (_playerInputController.inputActions.FirstPersonInputMap.Jump.triggered)
        {
            #region DropObject
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.transform.position = dropTarget.position;
            heldObject.transform.parent = null;
            heldObject = null;

            holdingObject = false;
            #endregion
        }
    }
}
