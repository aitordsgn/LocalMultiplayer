using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;


public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float driftFactor = 0.95f;
    [SerializeField] private float acceleraionFactor = 30.0f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float maxSpeed = 20;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;


    float rotationangle = 0;
    float velocityIsUp = 0;

    Rigidbody2D carrigidbody2D;
    private void Awake()
    {
        carrigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        ApplyEngineForce();

        KillOrtogonalVelocity();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        //Calculate how much forward we are going in terms of the direction of our velocity
        velocityIsUp = Vector2.Dot(transform.up, carrigidbody2D.velocity);
        //Limit so we cannot go faster than the max speed in forward direction
        if (velocityIsUp > maxSpeed && accelerationInput > 0)
            return;
        //Limit so we cannot go faster than the 5% of the max speed in reverse direction
        if (velocityIsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;
        //Limit so we cannot go faster in any direction while accelerating
        if (carrigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;
        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carrigidbody2D.drag = Mathf.Lerp(carrigidbody2D.drag, 3.0f, Time.deltaTime * 3);
        else carrigidbody2D.drag = 0;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * acceleraionFactor;

        //Aply force  and push the car forward
        carrigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    
    
    }

    private void ApplySteering()
    {

        
            //Limit the cars ability to turn when moving slowly
            float minSpeedforAllowTurningFactor = carrigidbody2D.velocity.magnitude / 8;
            minSpeedforAllowTurningFactor = Mathf.Clamp01(minSpeedforAllowTurningFactor);
            //Update rotationangle based on input
            rotationangle -= steeringInput * turnFactor * minSpeedforAllowTurningFactor;

            //Apply sterring by rotating the car object
            carrigidbody2D.MoveRotation(rotationangle); 
      
        
    }


    void KillOrtogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carrigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carrigidbody2D.velocity, transform.right);

        carrigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    public void OnLStickMove(InputAction.CallbackContext context)
    {
        Debug.Log("LStick");
        Vector2 inputVector = context.ReadValue<Vector2>().normalized;
        steeringInput = -inputVector.x;
        accelerationInput = inputVector.y;
        FindObjectOfType<AudioManager>().Play("C_Motor");

    }
   

    public float GetVelocityMagnitude()
    {
        return carrigidbody2D.velocity.magnitude;
    }
    public bool IsTireScreeching(out float lateralvelocity, out bool isBraking)
    {
        lateralvelocity = GetLateralVelocity();
        isBraking = false;

        //Checkear si se esta moviendo hacia delante y a la vez frenando
        if(accelerationInput <0f && velocityIsUp > 0)
        {
            isBraking = true;
            return true;
        }

        //If we have a lot of side movement then the tires should be streching
        if (Mathf.Abs(GetLateralVelocity()) > 2.0f)
            return true;
        

        return false;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carrigidbody2D.velocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("C_Choque");
        }
        else
            FindObjectOfType<AudioManager>().Play("C_Cono");

    }

    public void Frenar()
    {
        accelerationInput = -1;
    }

}
