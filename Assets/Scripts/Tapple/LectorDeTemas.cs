using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Hay que utilizar esta libreria para poder leer los txt con los temas.

public class LectorDeTemas : MonoBehaviour
{

    [SerializeField] private TextAsset textAssetTemas; //Hacemos la definicion de esta variable serialized para poder verlo desde el editor.


    [SerializeField] public string[] temas; //El array con los temas
    // Start is called before the first frame update
    void Start()
    {
        ReadTextAsset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadTextAsset()
    {
        temas = textAssetTemas.text.Split(new string[] { ".", "\n" }, StringSplitOptions.None);
    }


}
