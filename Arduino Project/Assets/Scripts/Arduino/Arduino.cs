using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

using System;

public class Arduino : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM8", 9600);
    bool isStreaming = false;
    bool ledOn = false;

    // Start is called before the first frame update
    void Start()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        sp.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStreaming)
        {
            string value = ReadSerialPort();
            if (value != null)
            {
                Debug.Log(value);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchLEDState();
            }
        }
    }

    void SwitchLEDState()
    {
        ledOn = !ledOn;
        sp.WriteLine((ledOn ? "L" : "H"));
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
