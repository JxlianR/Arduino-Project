using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

using System;


public class MovePlayer : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM8", 9600);
    bool isStreaming = false;

    Rigidbody2D rb;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        sp.Open();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isStreaming)
        {
            //Debug.Log("Hello World");
            string value = ReadSerialPort();
            if (value != null)
            {
                Debug.Log(value);
                Move(value);
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (isStreaming)
        {
            //Debug.Log("Hello World");
            string value = ReadSerialPort();
            if (value != null)
            {
                Debug.Log(value);
                Move(value);
            }
        }
    }

    void Move(string value)
    {
        int input = Int32.Parse(value);

        /// <summary>
        /// Threshold of 20cm
        /// if the hand is closer than that, the player moves to the left side
        /// if the hand is further away, the player moves to the right side
        /// </summary>
        
        /*if (input < 20)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-1, 0) * movementSpeed;
        }
        else
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(1, 0) * movementSpeed;
        }*/


        /// <summary>
        /// The closer the hand to the sensor, the faster the player moves to the left side
        /// if the hand is 14cm away the player is not moving at all
        /// at 15cm the player starts to move slowly to the right side
        /// And then the further the hands is away from the sensor, the faster the player moves to the right side (till 24cm)
        /// </summary>

        if (input <= 4)
        {
            Debug.Log("Left");
            rb.velocity = new Vector2(-CalculateInputValue(input), rb.velocity.y) * movementSpeed;
        }
        else if (input <= 24 && input > 14)
        {
            Debug.Log("Right");
            rb.velocity = new Vector2(input - 10, rb.velocity.y) * movementSpeed;
        }
        
    }

    int CalculateInputValue(int value)
    {
        int x = Math.Abs(value - 4 - 10);
        return x;
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

    void Close()
    {
        sp.Close();
    }

    string ReadSerialPort(int timeout = 10)
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