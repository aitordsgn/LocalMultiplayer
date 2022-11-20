using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int PlayerIndex;
    [SerializeField] TextMeshProUGUI titleText;

    [SerializeField] GameObject ReadyPanel, MenuPanel;

    //Color Selection
    [SerializeField] List<Color> Colores;

    [SerializeField] Image Personaje , FrameG_ , FrameS_;

    private int ColorSeleccionado = 0,SkinSeleccionado = 0;
         
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    [SerializeField]
    private Button ReadyButton;

    [SerializeField] private Sprite SpriteSeleccionado, SpriteNormal;

    private void Start()
    {
        if(PlayerIndex != 0)
        {
            ColorSeleccionado = Colores.Count;
            FrameG_.color = Colores[ColorSeleccionado];
            FrameS_.color = Colores[ColorSeleccionado];
            Personaje.color = Colores[ColorSeleccionado];
        }
    }
    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.SetText("Jugador " + (pi + 1).ToString());
        FindObjectOfType<AudioManager>().Play("MP_Join");

        ignoreInputTime = Time.time + ignoreInputTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    
    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        Debug.Log("Ready");
        PlayerConfigurationManager.Instance.setPlayerColor(PlayerIndex, Colores[ColorSeleccionado]);
        PlayerConfigurationManager.Instance.readyPlayer(PlayerIndex);
        ReadyButton.gameObject.SetActive(false);
        FrameG_.sprite = SpriteSeleccionado;
        FindObjectOfType<AudioManager>().Play("MP_Join");

    }

    public void NextColor()
    {
        Debug.Log("Siguiente Color");

        if (ColorSeleccionado >= Colores.Count)
        {
            ColorSeleccionado = 1;
        }
        else
        {
            ColorSeleccionado += 1;
        }
        //Personaje.color = Colores[ColorSeleccionado];
        FrameG_.color = Colores[ColorSeleccionado];
        FrameS_.color = Colores[ColorSeleccionado];
        Personaje.color = Colores[ColorSeleccionado];
        FindObjectOfType<AudioManager>().Play("MP_Wosh");

    }
    public void PrevColor()
    {
        Debug.Log("Color Anterior");

        if (ColorSeleccionado <= 1)
        {
            ColorSeleccionado = Colores.Count;
        }
        else
        {
            ColorSeleccionado -= 1;
        }
        FrameG_.color = Colores[ColorSeleccionado];
        FrameS_.color = Colores[ColorSeleccionado];
        Personaje.color = Colores[ColorSeleccionado];
        
        FindObjectOfType<AudioManager>().Play("MP_Wosh");

    }



}
