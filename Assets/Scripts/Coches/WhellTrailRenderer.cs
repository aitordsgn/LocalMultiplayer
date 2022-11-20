using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhellTrailRenderer : MonoBehaviour
{

    //Components
    TopDownCarController topDownCarController;
    TrailRenderer trailRenderer;

    private void Awake()
    {

        //Get the TopDownCarController
        topDownCarController = GetComponentInParent<TopDownCarController>();

        //Cogemos el TrailRenderer del mismo objeto
        trailRenderer = GetComponentInParent<TrailRenderer>();

        //Hacer que el trail no emita
        trailRenderer.emitting = false;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(topDownCarController.IsTireScreeching(out float latera, out bool isBraking))
        {
            trailRenderer.emitting = true;
            FindObjectOfType<AudioManager>().Play("C_Derrape");

        }
           
        else trailRenderer.emitting = false;
    }
}
