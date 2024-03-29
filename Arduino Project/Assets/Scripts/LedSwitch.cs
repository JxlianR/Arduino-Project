using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedSwitch : MonoBehaviour
{
    public float minTime, maxTime;

    [HideInInspector]
    public bool greenLedOn;

    Arduino arduino;

    // Start is called before the first frame update
    void Start()
    {
        arduino = FindObjectOfType<Arduino>();
        greenLedOn = true;
        StartSwitching();
    }

    void StartSwitching()
    {
        StartCoroutine(SwitchLed());
    }

    //Switch LED color after a random amount of time
    IEnumerator SwitchLed()
    {
        greenLedOn = !greenLedOn;
        arduino.WriteLine("L" + (greenLedOn ? "1" : "2"));
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        StartCoroutine(SwitchLed());
    }
}
