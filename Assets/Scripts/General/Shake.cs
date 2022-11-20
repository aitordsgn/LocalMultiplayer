using System.Collections;

using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start=false;
    public AnimationCurve curve;
    [SerializeField] float duration = 1f;
    

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            start = false;
            StartCoroutine(Shaking(Random.Range(0,1)));
        }
    }
    public void StartShake(float strengtha)
    {
        StartCoroutine(Shaking(strengtha));
    }

    IEnumerator Shaking(float strength)
    {
        Vector3 startPosition= transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float ease = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition+Random.insideUnitSphere * ease *strength;
            yield return null;
        }

        transform.position = transform.position;

    }


}
