using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMarkSpawner : MonoBehaviour
{
    private Vector2 lastPosition;
    public float trackDistance = 0.2f;
    public GameObject trackPrefab;
    public int objectPoolSize = 50;


    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        objectPool.Initialize(trackPrefab, objectPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position, lastPosition);

        if(distanceDriven >= trackDistance)
        {
            lastPosition = transform.position;
            var tracks = objectPool.CreateObject();
            tracks.transform.position = transform.position;
            tracks.transform.rotation = transform.rotation;
        }
    }
}
