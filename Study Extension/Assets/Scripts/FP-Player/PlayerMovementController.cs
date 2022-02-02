using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    
   [Header("Movement Settings")]
     [SerializeField] float walkSpeed;
     [SerializeField] float sprintSpeed;
     [SerializeField] float jumpForce;
     [SerializeField] SprintType sprintType;
     [SerializeField] float gravityScale = 1;
     [SerializeField] float damping = 0.3f;
     [SerializeField] bool airMovement;
     [SerializeField] bool isInAir = false;
     [SerializeField] bool isGrounded = false;
     [SerializeField] bool isSprinting = false;
     [SerializeField] float movementSpeed;
     [SerializeField] Rigidbody rb;
     
     public Transform mainCamera;
     public FirstPersonInputmanager playerInputController;
     Vector2 pInput;
     
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        //jumpLogic();
        sprintLogic();
    }
    private void FixedUpdate()
    {
        movementLogic();
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        isGrounded = Physics.Raycast(transform.position, -transform.up, 1.1f);
        if (isGrounded)
        {
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }
    }
    public enum SprintType
    {
        clickToSprint,
        holdToSprint
    }
    void jumpLogic()
    {
        if (isInAir == false)
        {
            if (playerInputController.inputActions.FirstPersonInputMap.Jump.triggered)
            {
                rb.velocity = new Vector3(rb.velocity.x,jumpForce*Time.fixedDeltaTime,rb.velocity.z);
                isInAir = true;
            }
        }
    }
    void sprintLogic()
    {
        if (isSprinting)
        {
            movementSpeed = sprintSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }
        switch (sprintType)
        {
            case SprintType.clickToSprint:
                if (playerInputController.inputActions.FirstPersonInputMap.Sprint.triggered)
                    isSprinting = !isSprinting;
                break;
            case SprintType.holdToSprint:
                playerInputController.inputActions.FirstPersonInputMap.Sprint.performed += sprint => isSprinting = true;
                playerInputController.inputActions.FirstPersonInputMap.Sprint.canceled += sprint => isSprinting = false;

                break;
        }
    }
    void movementLogic()
    {
        //set y rotation = camera y rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y, 0));
        
        pInput = playerInputController.inputActions.FirstPersonInputMap.Move.ReadValue<Vector2>();
    
        if (!isInAir)
        {
            rb.useGravity = false;
            Vector3 movement = Vector3.right * pInput.x * movementSpeed + Vector3.up * pInput.y * movementSpeed;
            
            rb.velocity = transform.right * movement.x + transform.forward * movement.y + transform.up * rb.velocity.y;
        }
        
        else
        {
            rb.useGravity = true;
            if (airMovement)
            {
                Vector3 movement = Vector3.right * pInput.x * movementSpeed + Vector3.up * pInput.y * movementSpeed;
                
                rb.velocity = transform.right * movement.x + transform.forward * movement.y + transform.up * rb.velocity.y;
            }
            
        }
    }
    
}
