using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerConfiguration playerconfig;
    //private Mover mover;

    [SerializeField] private Image frame;

    private InputActions controls;

    private void Awake()
    {
        //var mover = GetComponent<Mover>();
        controls = new InputActions();
    }


    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerconfig = pc;
        frame.color = pc.PlayerColor;

        playerconfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(obj.action.name == controls.Control.Mover.name)
        {
            Debug.Log("Mover");
        }
    }
}
