using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Platfomer : MonoBehaviour
{
    [SerializeField] private float EscalaVelocidad = 10f;
    Rigidbody2D PlayerRb2D;


    private void Awake()
    {
        PlayerRb2D = GetComponent<Rigidbody2D>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnLStickMove(InputAction.CallbackContext context)
    {
        Debug.Log("LStick");
        Vector2 vector2 = context.ReadValue<Vector2>();
        vector2 = new Vector2(vector2.x * EscalaVelocidad, PlayerRb2D.velocity.y);
        PlayerRb2D.velocity = vector2;
    }
}
