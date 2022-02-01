using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonInputmanager : MonoBehaviour
{
    public PlayerInput inputActions;
    private void Awake()
    {
        inputActions = new PlayerInput();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

}
