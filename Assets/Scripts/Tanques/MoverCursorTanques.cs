using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoverCursorTanques : MonoBehaviour
{

    [SerializeField] private float VelocidadCursorMax;
    public void OnLStickMove(InputAction.CallbackContext context)
    {
        this.gameObject.transform.position += new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 0f) * VelocidadCursorMax * Time.deltaTime;
    }
}
