using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedSwitch : MonoBehaviour
{
    public float minTime, maxTime;

    bool greenLedOn;

    Arduino arduino;

    // Start is called before the first frame update
    void Start()
    {
        arduino = FindObjectOfType<Arduino>();
        greenLedOn = false;
        arduino.WriteLine("Red");
        StartSwitching();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSwitching()
    {
        InvokeRepeating("SwitchLed", 0f, Random.Range(minTime, maxTime));
    }

    void SwitchLed()
    {
        if (greenLedOn == true)
        {
            arduino.WriteLine("Red");
            greenLedOn = false;
        }
        else
        {
            arduino.WriteLine("Green");
            greenLedOn = true;
        }
    }
}
