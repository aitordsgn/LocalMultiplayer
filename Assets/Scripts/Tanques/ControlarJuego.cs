using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("T_Silbato");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
