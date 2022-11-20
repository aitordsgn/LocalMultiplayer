using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BotonTapple : MonoBehaviour
{
    [SerializeField] private Juego juego;
    [SerializeField] private GameObject Usada, SiguienteLetra;
    [SerializeField] private GameObject[] BotonesLetras;
    [SerializeField] private int Orden;
    [SerializeField] private Animator TurnoIzda, TurnoDcha;

    [SerializeField] private GameObject Particulas;


    private void Start()
    {
        for (int i = 0; i < BotonesLetras.Length; i++)
        {
            if (gameObject == BotonesLetras[i])
            {
                Orden = i;
                break;
            }

        }
    }

    public void LetraUsada()
    {
        Usada.SetActive(true); //Activamos la raya que tacha la letra
        gameObject.GetComponent<Button>().interactable = false; //Hacemos que la letra no pueda utilizarse
        Instantiate(Particulas, this.transform);
        //Utilizamos un for para encontrar el
        bool VueltaDada = false;
        for (int i = Orden; i < BotonesLetras.Length; i++)
        {
            if (BotonesLetras[i].GetComponent<Button>().interactable)
            {
                SiguienteLetra = BotonesLetras[i];
                break;
            }
            if (i == BotonesLetras.Length-1 && VueltaDada == false) {            
                VueltaDada = true;
                i = -1;
            }

        }
        FindObjectOfType<AudioManager>().Play("Click");
        TurnoIzda.SetTrigger("CambioTurno");
        Debug.Log("CambioTurno");
        TurnoDcha.SetTrigger("CambioTurno");
        juego.ReiniciarTiempo();
        //Limpiar el objeto seleccionado
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(SiguienteLetra);
    }

    
}
