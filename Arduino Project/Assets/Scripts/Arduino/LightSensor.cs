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
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        //sp.Open();

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject textObject = new GameObject("LightFeedback");
        textObject.transform.parent = transform;

        textMesh = textObject.AddComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.characterSize = 0.5f; 

        StartCoroutine(ChangeLights());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isStreaming)
        {
            string value = ReadSerialPort();
            if (lightsOut == true && value == "LightsOn")
            {
                Debug.Log(value);
                TurnOnLights();
            }
        }*/
    }

    public void TurnOnLights()
    {
        if (lightsOut == false) return;

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

        textMesh.text = "Oh no, lights went off.\nMaybe there's something I can do.";
        Camera mainCamera = Camera.main;
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
        worldCenter.z = -1f; 
        textMesh.transform.position = worldCenter;

         yield return new WaitForSeconds(3f);
        textMesh.text = string.Empty;

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
