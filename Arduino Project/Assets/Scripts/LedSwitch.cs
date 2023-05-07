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
        greenLedOn = true;
        //arduino.WriteLine("Red");
        StartSwitching();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSwitching()
    {
        //InvokeRepeating("SwitchLed", 0f, Random.Range(minTime, maxTime));
        StartCoroutine(SwitchLed());
    }

    IEnumerator SwitchLed()
    {
        Debug.Log("SwitchLed Called");

        if (greenLedOn == true)
        {
            Debug.Log("Switched to Red");
            arduino.WriteLine("L" + "0");
            greenLedOn = false;
        }
        else
        {
            Debug.Log("Switched to Green");
            arduino.WriteLine("L" + "1");
            greenLedOn = true;
        }

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        StartCoroutine(SwitchLed());
    }
}
