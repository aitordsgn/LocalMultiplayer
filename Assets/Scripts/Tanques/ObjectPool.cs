using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;

    [SerializeField] protected Queue<GameObject> objectPool;

    public Transform spawnedObjectsParents;


    private void Awake()
    {
        objectPool = new Queue<GameObject>();

    }

    public void Initialize(GameObject objectToPool,int poolsize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolsize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNedded();
        GameObject spawnedObject = null;

        if(objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParents);
            spawnedObject.AddComponent<DestroyIfDisabled>();
        }

        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNedded()
    {
     if(spawnedObjectsParents == null)
        {
            string name = "ObjectPool" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if(parentObject != null)
            {
                spawnedObjectsParents = parentObject.transform;
            }
            else
            {
                spawnedObjectsParents = new GameObject(name).transform;
            }
        }
    }

    private void OnDestroy()
    {
        foreach(var item in objectPool)
        {
            if (item == null)
                continue;
            else if (item.activeSelf == false)
                Destroy(item);
            else
                item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
        }
    }
}
 