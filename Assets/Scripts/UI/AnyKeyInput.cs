using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class AnyKeyInput : MonoBehaviour
{
    [SerializeField] private InputActions controls;
    [SerializeField] private PlayerInput input;
    private void Awake()
    {
      controls = new InputActions();
      input.onActionTriggered += Input_onActionTriggered;

    }
    private void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log("Triggered");

        if (obj.action.triggered)
        {
            Debug.Log("Triggered");
            FindObjectOfType<LevelLoader>().LoadNextLevel(1);
        }
    }
}
