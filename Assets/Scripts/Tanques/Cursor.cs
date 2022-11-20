using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Transform Tanque;

    [SerializeField] private LineRenderer Linea;
    // Start is called before the first frame update
    void Start()
    {
        Linea = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Linea.SetPosition(0, this.transform.position);
        Linea.SetPosition(1, Tanque.position);
    }
}
