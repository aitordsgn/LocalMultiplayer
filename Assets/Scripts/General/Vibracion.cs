using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public enum RumblePattern
{
    Constant,
    Pulse,
    Linear
}

public class Vibracion : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private RumblePattern activeRumbePattern;
    [SerializeField] private float rumbleDurration;
    [SerializeField] private float pulseDurration;
    [SerializeField] private float lowA;
    [SerializeField] private float lowStep;
    [SerializeField] private float highA;
    [SerializeField] private float highStep;
    [SerializeField] private float rumbleStep;
    [SerializeField] private bool isMotorActive = false;

    // Public Methods
    public void RumbleConstant(float low, float high, float durration)
    {
        activeRumbePattern = RumblePattern.Constant;
        lowA = low;
        highA = high;
        rumbleDurration = Time.time + durration;
    }

    public void RumblePulse(float low, float high, float burstTime, float durration)
    {
        Debug.Log("Rumble");
        activeRumbePattern = RumblePattern.Pulse;
        lowA = low;
        highA = high;
        rumbleStep = burstTime;
        pulseDurration = Time.time + burstTime;
        rumbleDurration = Time.time + durration;
        isMotorActive = true;
        var g = GetGamepad();
        g?.SetMotorSpeeds(lowA, highA);
    }

    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float durration)
    {
        activeRumbePattern = RumblePattern.Linear;
        lowA = lowStart;
        highA = highStart;
        lowStep = (lowEnd - lowStart) / durration;
        highStep = (highEnd - highStart) / durration;
        rumbleDurration = Time.time + durration;
    }

    public void StopRumble()
    {
        var gamepad = GetGamepad();
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }


    // Unity MonoBehaviors
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (Time.time > rumbleDurration)
        {
            StopRumble();
            return;
        }

        var gamepad = GetGamepad();
        if (gamepad == null)
            return;
        
        switch (activeRumbePattern)
        {
            case RumblePattern.Constant:
                gamepad.SetMotorSpeeds(lowA, highA);
                break;

            case RumblePattern.Pulse:

                if (Time.time > pulseDurration)
                {
                    isMotorActive = !isMotorActive;
                    pulseDurration = Time.time + rumbleStep;
                    if (!isMotorActive)
                    {
                        gamepad.SetMotorSpeeds(0, 0);
                    }
                    else
                    {
                        gamepad.SetMotorSpeeds(lowA, highA);
                    }
                }

                break;
            case RumblePattern.Linear:
                gamepad.SetMotorSpeeds(lowA, highA);
                lowA += (lowStep * Time.deltaTime);
                highA += (highStep * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        StopRumble();
    }

    // Private helpers

   private Gamepad GetGamepad()
    {
        //return Gamepad.all.FirstOrDefault(g => _playerInput.devices.Any(d => d.deviceId == g.deviceId));
        //return null;
        #region Linq Query Equivalent Logic
        Gamepad gamepad = null;
        foreach (var g in Gamepad.all)
        {
            foreach (var d in _playerInput.devices)
            {
                if (d.deviceId == g.deviceId)
                {
                    gamepad = g;
                    break;
                }
            }
            if (gamepad != null)
            {
                break;
            }
        }
        return gamepad;
        #endregion
    }
   
    public void SetplayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }
}