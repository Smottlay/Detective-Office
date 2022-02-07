using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PresentationScript : MonoBehaviour
{
    public Canvas[] cards;
    public int arrayCount = 0;

    
    public void Update()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            arrayCount++;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            arrayCount--;
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(1);
        }

        CardSelect();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (arrayCount > 5)
        {
            arrayCount = 0;
        }

        if (arrayCount < 0)
        {
            arrayCount = 5;
        }
    }

    private void CardSelect()
    {
        if (arrayCount == 0)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[0].enabled = true;
        }
        else if (arrayCount == 1)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[1].enabled = true;
        }
        else if (arrayCount == 2)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[2].enabled = true;
        }
        else if (arrayCount == 3)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[3].enabled = true;
        }
        else if (arrayCount == 4)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[4].enabled = true;
        }
        else if (arrayCount == 5)
        {
            foreach (Canvas canvas in cards)
            {
                canvas.enabled = false;
            }

            cards[5].enabled = true;
        }
    }
}
