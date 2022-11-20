using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Rondas : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject PantallaFinal;

    [Header("Variables")]
    //Array que contiene las puntuaciones
    [SerializeField] private int[] Puntuaciones;
    //Int que contiene las maximas rondas que se pueden tener
    [SerializeField] private int PuntuacionMaxima;

    [SerializeField] private List<PlayerConfiguration> PlayerConfigs;
    public void Acabado(int GanadorIndex)
    {
        //Activamos la Pantalla final,
        PantallaFinal.SetActive(true);


    }

    public void Configurar(int pPuntuacionMaxima)
    {
        pPuntuacionMaxima = PuntuacionMaxima;
    }
}
