using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class Interact : MonoBehaviour
{
    public FirstPersonInputmanager _playerInputController;
    public Drawer _drawer;
    
    public Transform objTarget;
    public Transform dropTarget;

    public GameObject heldObject;
    public bool holdingObject;
    public float pickupDistance;
    public float drawerDistance;
    
    private void Start()
    {
        Physics.IgnoreLayerCollision(6,3);
        holdingObject = false;
    }

    public void Update()
    {
        HandleAllCustomFunctions();
    }
    
    public void HandleAllCustomFunctions()
    {
        PickupSystem();
        DropObject();
        
        OpenCloseDrawer();
    }
    private void PickupSystem()
    {
        RaycastHit hit;
        Ray raycast = new Ray(transform.position, transform.forward);
        
        if (_playerInputController.inputActions.FirstPersonInputMap.Fire.triggered)
        {
            if (Physics.Raycast(raycast, out hit, pickupDistance))
            {
                /*
                print("Found an object - distance: " + hit.distance);
                Debug.DrawLine(raycast.origin, hit.point, Color.blue, 200);
                */

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

    private void OpenCloseDrawer()
    {
        RaycastHit drawHit;
        Ray drawerRaycast = new Ray(transform.position, transform.forward);

        if (_playerInputController.inputActions.FirstPersonInputMap.Fire.triggered)
        {
            if (Physics.Raycast(drawerRaycast,  out drawHit, drawerDistance))
            {
                if (drawHit.transform.CompareTag("Drawer"))
                {
                    _drawer.toggleDrawer = true;
                }
            }
        }
    }
}

