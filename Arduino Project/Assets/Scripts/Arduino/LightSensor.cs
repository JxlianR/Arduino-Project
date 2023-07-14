using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class LightSensor : MonoBehaviour
{
    bool lightsOut;
    public int minSeconds, maxSeconds;

    SpriteRenderer spriteRenderer;
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
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

    }

    public void TurnOnLights()
    {
        if (lightsOut == false) return;

        //Turning lights back on
        spriteRenderer.enabled = false;
        lightsOut = false;

        //Turning lights off
        StartCoroutine(ChangeLights());
    }

    IEnumerator ChangeLights()
    {
        // Wait for a random amount of time
        int waitSeconds = UnityEngine.Random.Range(minSeconds, maxSeconds);
        yield return new WaitForSeconds(waitSeconds);

        // Turn lights off
        spriteRenderer.enabled = true;
        lightsOut = true;

        // Text Feedback
        textMesh.text = "Oh no, lights went off.\nMaybe there's something I can do.";
        Camera mainCamera = Camera.main;
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
        worldCenter.z = -1f; 
        textMesh.transform.position = worldCenter;

         yield return new WaitForSeconds(3f);
        textMesh.text = string.Empty;

    }
}
