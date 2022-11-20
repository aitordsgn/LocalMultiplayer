using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    public GameObject SpriteMuerte;
    public void DestroyHelper()
    {
        Instantiate(SpriteMuerte, transform.position, Quaternion.EulerAngles(0,0,Random.Range(0,360)));
        Destroy(gameObject);
    }
}
