using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2D;

    [SerializeField] private float maxSpeed = 10;

    [SerializeField] private float rotationSpeed = 100;

    private Vector2 movementVector;

    [SerializeField] private float acceleration = 70, deaceleration = 50, currentSpeed = 0, currentForwardDirection=1; 


    private void Awake()
    {
        rb2D = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //rb2D.velocity = (Vector2)transform.up * currentSpeed  * currentForwardDirection* Time.fixedDeltaTime;
        rb2D.velocity = (Vector2)transform.up * currentSpeed  * currentForwardDirection* Time.fixedDeltaTime;
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void OnLStickMove(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
            currentForwardDirection = 1;
        else if(movementVector.y < 0)
            currentForwardDirection = -1;
        FindObjectOfType<AudioManager>().Play("T_Motor");

    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed += deaceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
}
