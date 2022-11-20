using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Juego;
using UnityEngine.EventSystems;

public class MenuPausa : MonoBehaviour

{
    [SerializeField] private GameObject Pausa, Opciones;
    [SerializeField] private GameObject BotonPrevPausa;
    public GameObject PauseMenu, OptionsMenu, Game1, Game2;

    public GameObject pauseFirstButton, OptionFirstButton, OptionCloseButton;
    [SerializeField] Juego juego;
    public void PauseUnpause(GameObject botonPrevPausa)
    {
       

        if (!PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            //Limpiar el objeto seleccionado
            EventSystem.current.SetSelectedGameObject(null);
            //Setear un nuevo objeto seleccionado
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else
        {
            Debug.Log(botonPrevPausa);
            //Limpiar el objeto seleccionado
            EventSystem.current.SetSelectedGameObject(null);
            //Setear un nuevo objeto seleccionado
            EventSystem.current.SetSelectedGameObject(botonPrevPausa);
            BotonPrevPausa = botonPrevPausa;
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            OptionsMenu.SetActive(false);
        }
    }


    public void PausaUnpausaBoton()
    {
        //Limpiar el objeto seleccionado
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(BotonPrevPausa);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        OptionsMenu.SetActive(false);
        Game1.SetActive(true);
        Game2.SetActive(true);
        juego.state = juego.prevstate;

    }
    public void OpenOptions()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        //Limpiar el objeto seleccionado
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(OptionFirstButton);
    }
    public void CloseOptions()
    {
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //Setear un nuevo objeto seleccionado
        EventSystem.current.SetSelectedGameObject(OptionCloseButton);

    }
}
