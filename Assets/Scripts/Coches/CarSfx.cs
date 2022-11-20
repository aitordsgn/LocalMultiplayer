using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSfx : MonoBehaviour
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource tireScreeachingAudioSource, EngineAudioSource, CarHitAudioSource;


    //Conponents
    TopDownCarController topDownCarController;
    float desiredEnginePitch = 1f;
    float tireStreechPitch;
    private void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnineSFX();
        UpdateTiresScreechingSFX();
    }

    private void UpdateEnineSFX ()
    {
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();

        float desiredEngineVolume = Mathf.Abs(velocityMagnitude * 0.05f);

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);
        EngineAudioSource.volume = Mathf.Lerp(EngineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        EngineAudioSource.pitch = desiredEnginePitch;
    }

    private void UpdateTiresScreechingSFX()
    {
        if (topDownCarController.IsTireScreeching(out float lateralvelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tireScreeachingAudioSource.volume = Mathf.Lerp(tireScreeachingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                tireStreechPitch = Mathf.Lerp(tireStreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                tireScreeachingAudioSource.volume = Mathf.Abs(lateralvelocity) * 0.05f;
                tireStreechPitch = Mathf.Abs(lateralvelocity) * 0.1f;
            }
        }
        //Fade out the volume
        else tireScreeachingAudioSource.volume = Mathf.Lerp(tireScreeachingAudioSource.volume, 0, Time.deltaTime*10);
    }
}
