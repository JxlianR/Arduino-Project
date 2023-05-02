using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class LightSensor : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM8", 9600);
    bool isStreaming = false;

    bool lightsOut;
    public int alpha = 222;
    public int minSeconds, maxSeconds;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        sp.Open();

        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeLights());
    }

    // Update is called once per frame
    void Update()
    {
        if (isStreaming)
        {
            string value = ReadSerialPort();
            if (lightsOut == true && value == "LightsOn")
            {
                Debug.Log(value);
                TurnOnLights();
            }
        }
    }

    void TurnOnLights()
    {
        spriteRenderer.enabled = false;
        lightsOut = false;
        StartCoroutine(ChangeLights());
    }

    IEnumerator ChangeLights()
    {
        int waitSeconds = UnityEngine.Random.Range(minSeconds, maxSeconds);
        yield return new WaitForSeconds(waitSeconds);
        spriteRenderer.enabled = true;
        lightsOut = true;
    }

    void Close()
    {
        sp.Close();
    }

    string ReadSerialPort(int timeout = 50)
    {
        string message;
        sp.ReadTimeout = timeout;

        try
        {
            message = sp.ReadLine();
            return message;
        }
        catch (TimeoutException)
        {
            return null;
        }
    }
}
