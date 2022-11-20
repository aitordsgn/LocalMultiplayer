using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDespuesDeTiempo : MonoBehaviour
{
    [SerializeField] private float Retardo;
    [SerializeField] private TopDownCarController TopDownCarController;

    private void Start()
    {
        StartCoroutine(DestruirRetardado(Retardo));
    }
    IEnumerator DestruirRetardado(float Retardoc)
    {
        Debug.Log("Destruyendo");
        yield return new WaitForSeconds(Retardoc);
        Debug.Log("Destruido");
        TopDownCarController.enabled = true;
    }
}
