using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform minuteHand;
    public Transform hourHand;

    private void Update()
    {
        DateTime currentTime = DateTime.Now;
        float minute = (float) currentTime.Minute;
        float hour = (float) currentTime.Hour;

        float minuteAngle = 360 * (minute/60);
        float hourAngle = 360 * (hour/12);
        
        minuteHand.localRotation = Quaternion.Euler(0,0,minuteAngle);
        hourHand.localRotation = Quaternion.Euler(0,0,hourAngle);
    }
}
