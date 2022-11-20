using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSemaforo : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Tiempo());
    }
    IEnumerator Tiempo()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("C_S1");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("C_S1");
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Play("C_S2");

    }
}
