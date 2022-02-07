using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToBuild : MonoBehaviour
{
    public PresentationScript presentationScript;
    
    public void GoToBuild()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToWeaponsCard()
    {
        presentationScript.arrayCount = 2;
    }

    public void GoToOfficeCard()
    {
        presentationScript.arrayCount = 3; 
    }

    public void GoToPlanCard()
    {
        presentationScript.arrayCount = 4;
    }
}
