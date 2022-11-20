using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMaterialSegunPadre : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Padre;
    // Update is called once per frame
    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().color = Padre.color;
        if (GetComponent<TrailRenderer>() != null)
        {
            GetComponent<TrailRenderer>().endColor = Padre.color;
            GetComponent<TrailRenderer>().startColor = Padre.color;
        }
    }
    void Update()
    {
       
    }
}
