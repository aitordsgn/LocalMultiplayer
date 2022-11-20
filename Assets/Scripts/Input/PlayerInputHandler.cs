using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Referencias Scripts")]
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private Movement mover;
    [SerializeField] private AimTurret torreta;
    [SerializeField] private Turret shoot;
    [SerializeField] private Vibracion vibracion;
    [SerializeField] private TopDownCarController topDownCarController;
    [SerializeField] private Platfomer platfomer;

    [Header("Referencias Varias")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer J1; 
    [SerializeField] private Sprite  SpriteJ2;

    private InputActions controls;
    private void Awake()
    {
        mover = GetComponentInChildren<Movement>();
        torreta = GetComponentInChildren<AimTurret>();
        shoot = GetComponentInChildren<Turret>();
        vibracion = GetComponentInChildren<Vibracion>();
        topDownCarController = GetComponentInChildren<TopDownCarController>();
        platfomer =GetComponentInChildren<Platfomer>();


        controls = new InputActions();

    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        spriteRenderer.color = pc.PlayerColor;
        if(pc.PlayerIndex==1)
        {
            J1.sprite = SpriteJ2;
        }
        playerConfig.Input.SwitchCurrentActionMap("Tanque");
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        //vibracion.SetplayerInput(playerConfig.Input);

    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log("Action is triggered " + obj.action.name);
        if(obj.action.name == controls.Tanque.Disparar.name)
        {
            Debug.Log("Accionando " + obj.action.name);
            if (shoot != null)
                shoot.Shoot();
            if (topDownCarController != null)
                topDownCarController.Frenar();

        }
        if (obj.action.name == controls.Tanque.Frenar.name)
        {
            Debug.Log("Accionando " + obj.action.name);
            if (topDownCarController != null)
                topDownCarController.Frenar();
        }
        if (obj.action.name == controls.Tanque.MoverTorreta.name)
        {
            if (torreta != null)
                torreta.OnRStickMove(obj);
        }
        if (obj.action.name == controls.Tanque.MoverTanque.name)
        {
            if (mover != null)
                mover.OnLStickMove(obj);
            if(topDownCarController !=null)
                topDownCarController.OnLStickMove(obj);
            if(platfomer != null)
                platfomer.OnLStickMove(obj);

        }
    }
}
