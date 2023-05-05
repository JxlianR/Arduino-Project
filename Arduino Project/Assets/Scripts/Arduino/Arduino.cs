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

    Player player;
    LightSensor lightSensor;

    // Start is called before the first frame update
    void Start()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        sp.Open();

        player = FindObjectOfType<Player>();
        lightSensor = FindObjectOfType<LightSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStreaming)
        {
            string value = ReadSerialPort();
            Debug.Log(value);
            if (value != null)
            {
                switch (value)
                {
                    case "LightsOn":
                        lightSensor.TurnOnLights();
                        break;
                    case "PickUpPowerUp":
                        player.PickupPowerUp();
                        break;
                    default:
                        player.Move(int.Parse(value));
                        break;
                }
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
