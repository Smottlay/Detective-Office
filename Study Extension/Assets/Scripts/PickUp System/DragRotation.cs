using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragRotation : MonoBehaviour
{
    private void OnMouseDrag()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        float xAxis = mousePosition.x;
        float yAxis = mousePosition.y;
        
        transform.RotateAround (Vector3.down, xAxis);
        transform.RotateAround (Vector3.right, yAxis);
    }
}
