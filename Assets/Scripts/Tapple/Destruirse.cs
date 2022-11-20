using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruirse : MonoBehaviour
{
    [SerializeField] private float SegundosDeEspera = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySlef());
    }

    IEnumerator DestroySlef()
    {
        yield return new WaitForSeconds(SegundosDeEspera);
        Destroy(this.gameObject);
    }
}
