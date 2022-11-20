using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{

    float particleemissionRate = 0;

    TopDownCarController topDownCarController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        particleSystemSmoke = GetComponent<ParticleSystem>();

        emissionModule = particleSystemSmoke.emission;


        emissionModule.rateOverTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particleemissionRate = Mathf.Lerp(particleemissionRate, 0, Time.deltaTime*5);
        emissionModule.rateOverTime = particleemissionRate;


        if (topDownCarController.IsTireScreeching(out float lateralvelocity, out bool isBraking))
        {
            if (isBraking)
                particleemissionRate = 30;
            else particleemissionRate = Mathf.Abs(lateralvelocity) * 2;
        }
    }
}
