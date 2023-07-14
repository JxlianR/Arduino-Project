using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

using System;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    Score score;

    // Move variables
    public float movementSpeed, speedBoost;
    float timeSurvived;

    // Variables for making player invincible
    public LayerMask playerLayer, obstacleLayer;
    public float invincibilityAlpha;
    SpriteRenderer spriteRenderer;

    // Powerup related Variables
    [HideInInspector]
    public bool powerUpAvailable;
    public float powerUpDuration;
    PowerUpPickup.PowerUpType powerUpType;
    public Text powerUpText;
    public LedSwitch led;

    [HideInInspector]
    public bool invincible;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        //Increse speed the longer the player survives
        timeSurvived += Time.deltaTime;
        movementSpeed += timeSurvived * 0.0005f;
        Debug.Log(movementSpeed);
    }

    public void Move(int input)
    {
        /// <summary>
        /// Threshold of 15cm
        /// if the hand is closer than that, the player moves to the left side
        /// if the hand is further away, the player moves to the right side
        /// </summary>

        if (input <= 15)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (input >= 16)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }


        /// <summary>
        /// The closer the hand to the sensor, the faster the player moves to the left side
        /// if the hand is 14cm away the player is not moving at all
        /// at 15cm the player starts to move slowly to the right side
        /// And then the further the hands is away from the sensor, the faster the player moves to the right side (till 24cm)
        /// </summary>

        /*if (input <= 4)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-CalculateInputValue(input), rb.velocity.y);
        }
        else if (input <= 24 && input > 14)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2((input - 14), rb.velocity.y);
        }*/

    }

    int CalculateInputValue(int value)
    {
        int x = Math.Abs(value - 4 - 10);
        return x;
    }

    public void PickupPowerUp(PowerUpPickup.PowerUpType powerUpType)
    {
        this.powerUpType = powerUpType;
        powerUpAvailable = true;
        powerUpText.text = powerUpType.ToString();
    }

    public void ActivatePowerUp()
    {
        // Handle the power-up logic based on the powerUpType
        if (powerUpAvailable == true && led.greenLedOn == true)
        {
            switch (powerUpType)
            {
                case PowerUpPickup.PowerUpType.SpeedBoost:
                    // Apply speed boost logic
                    movementSpeed += speedBoost;
                    Debug.Log("Player picked up Speed Boost!");
                    break;
                case PowerUpPickup.PowerUpType.Invincibility:
                    // Apply invincibility logic
                    Invincible();
                    Debug.Log("Player picked up Invincibility!");
                    break;
                case PowerUpPickup.PowerUpType.ScoreMultiplier:
                    // Apply score multiplier logic
                    score.multiplayer = 2;
                    Debug.Log("Player picked up Score Multiplier!");
                    break;
                default:
                    // Handle unrecognized power-up type
                    Debug.LogWarning("Unrecognized power-up type!");
                    break;
            }

            StartCoroutine(CancelPowerUpEffect(powerUpType));
            powerUpAvailable = false;

             // Clear the power-up text after activating
             powerUpText.text = "";
        }
    }

    void Invincible()
    {
        invincible = true;

        // Ignore collision with obstacles
        Physics2D.IgnoreLayerCollision(6, 7, true);

        //Change color of player (only alpha value)
        Color newColor = spriteRenderer.color;
        newColor.a = invincibilityAlpha;
        spriteRenderer.color = newColor;
    }

    //Return to normal state
    void CancelInvincibility()
    {
        invincible = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Color newColor = spriteRenderer.color;
        newColor.a = 255;
        spriteRenderer.color = newColor;
    }

    IEnumerator CancelPowerUpEffect(PowerUpPickup.PowerUpType powerUpType)
    {
        yield return new WaitForSeconds(powerUpDuration);

        switch (powerUpType)
        {
            case PowerUpPickup.PowerUpType.SpeedBoost:
                // Apply speed boost logic
                movementSpeed -= speedBoost;
                Debug.Log("Speed Boost expired!");
                break;
            case PowerUpPickup.PowerUpType.Invincibility:
                CancelInvincibility();
                Debug.Log("Invincibility expired!");
                break;
            case PowerUpPickup.PowerUpType.ScoreMultiplier:
                // Apply score multiplier logic
                score.multiplayer = 1;
                Debug.Log("Score Multiplier expired!");
                break;
            default:
                // Handle unrecognized power-up type
                Debug.LogWarning("Unrecognized power-up type!");
                break;
        }
    }
}
