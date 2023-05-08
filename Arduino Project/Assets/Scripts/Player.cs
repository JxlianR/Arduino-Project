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
    public float movementSpeed, powerMovementSpeed;
    float normalMovementSpeed;

    // Jump variables
    bool isGrounded;
    public Transform playerFeet;
    public LayerMask groundLayer;

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
        //isGrounded = Physics2D.OverlapCircle(playerFeet.position, 0.1f, groundLayer);
    }

    public void Move(int input)
    {
        /// <summary>
        /// Threshold of 20cm
        /// if the hand is closer than that, the player moves to the left side
        /// if the hand is further away, the player moves to the right side
        /// </summary>

        if (input < 20)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (input <= 40)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        //else if (isGrounded)
            //Jump();


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
                    normalMovementSpeed = movementSpeed;
                    movementSpeed = powerMovementSpeed;
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
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
        Color newColor = spriteRenderer.color;
        newColor.a = invincibilityAlpha;
        spriteRenderer.color = newColor;
    }

    void CancelInvincibility()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
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
                movementSpeed = normalMovementSpeed;
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

    /* 0 = 10
     * 1 = 9
     * 2 = 8
     * 3 = 7
     * 4 = 6
     * 5 = 5
     * 6 = 4
     * 7 = 3
     * 8 = 2
     * 9 = 1
     * 10 = 0
     * 
     *  value + x = 10
     *  value - 10 = x
     *  Math.Abs(x);
     *  
     *  x = inputValue
     *  
     */

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Power-UP")
        {
            powerUpAvailable = true;
            nearPowerUp = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Power-UP")
        {
            powerUpAvailable = false;
            nearPowerUp = null;
        }
    }*/
}
