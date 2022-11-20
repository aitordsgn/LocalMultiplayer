using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{


    [SerializeField] private float VelocidadMovimiento;


    [SerializeField] private Rigidbody2D rb2d;

    private Vector2 movementDirection;

    [SerializeField] private float RotationSpeed;

    private float rotacionAnt;

    private void FixedUpdate()
    {
        Move();
    }


    public void OnLStickMove(InputAction.CallbackContext context)
    {
         movementDirection = context.ReadValue<Vector2>();
        FindObjectOfType<AudioManager>().Play("T_Motor");

    }

    void Move()
    {
        //Movimiento Tanque
        var movimiento = movementDirection.normalized;
        rb2d.velocity = new Vector2(-movimiento.x *Time.deltaTime * VelocidadMovimiento, movimiento.y * Time.deltaTime * VelocidadMovimiento);


        //Rotacion Tanque
        rotacionAnt = rb2d.rotation;
        var rotacion = rb2d.velocity;
        var angleRad  = Mathf.Atan2(rotacion.y, -rotacion.x);
        float angleDeg  = angleRad * Mathf.Rad2Deg;
        Debug.Log(angleDeg);
        if (rb2d.velocity.x > 0)
        {
            rb2d.rotation = Mathf.Lerp(rb2d.rotation,(Mathf.Atan(rb2d.velocity.y / rb2d.velocity.x) * Mathf.Rad2Deg) - 90,RotationSpeed);

        }
        else
        {
            rb2d.rotation = Mathf.Lerp(rb2d.rotation, (Mathf.Atan(rb2d.velocity.y / rb2d.velocity.x) * Mathf.Rad2Deg) + 90, RotationSpeed);

        }
    }
    

   
    
}
