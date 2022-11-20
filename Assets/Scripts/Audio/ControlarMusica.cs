using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarMusica : MonoBehaviour
{
    [SerializeField] private string NombreMusica;
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic(NombreMusica);
        Debug.Log("Se está tocando:" + NombreMusica);
    }
}
