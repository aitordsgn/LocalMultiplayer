using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarSinParar : MonoBehaviour
{
    [SerializeField] private RectTransform transform;
    [SerializeField] private float VelocidadDeRotacion;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, VelocidadDeRotacion * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
