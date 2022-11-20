using System.Collections;
using UnityEngine;


public class DestruirConRetardo : MonoBehaviour
{
    [SerializeField] private float Retardo;


    private void Start()
    {
        StartCoroutine(DestruirRetardado(Retardo));
    }
    IEnumerator DestruirRetardado(float Retardoc)
    {
        Debug.Log("Destruyendo");
        yield return new WaitForSeconds(Retardoc);
        Debug.Log("Destruido");
        Destroy(this.gameObject);
    }
}
