using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TMP_Text scoreText;

    [HideInInspector]
    public float multiplayer = 1;
    float score;


    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score: " + Mathf.Round(score).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score += 1 * Time.deltaTime * multiplayer;
        scoreText.text = "Score: " + Mathf.Round(score).ToString();
    }
}
