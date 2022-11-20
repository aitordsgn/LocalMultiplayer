using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandlerCar : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private TopDownCarController topDownCarController;
    [SerializeField] private SpriteRenderer spriteRenderer, J1;
    [SerializeField] private Sprite SpriteJ2;

    private InputActions controls;
    private void Awake()
    {
        topDownCarController = GetComponentInChildren<TopDownCarController>();
        controls = new InputActions();

    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        spriteRenderer.color = pc.PlayerColor;
        if (pc.PlayerIndex == 1)
        {
            J1.sprite = SpriteJ2;
        }
        playerConfig.Input.SwitchCurrentActionMap("Tanque");
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log("Action is triggered " + obj.action.name);
        if (obj.action.name == controls.Tanque.Disparar.name)
        {
            

        }
        if (obj.action.name == controls.Tanque.MoverTanque.name)
        {
            
            topDownCarController.OnLStickMove(obj);

        }
    }
}
