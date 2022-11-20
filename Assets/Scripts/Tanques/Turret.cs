using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    public List<Transform> TurretBullets;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canshoot = true;
    private Collider2D[] tankColliders;
    public float currentDelay = 0;


    private ObjectPool bulletPool;

    [SerializeField] private int bulletPoolCount = 10;
    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initialize(bulletPrefab, bulletPoolCount);
    }
    private void Update()
    {
        if(canshoot==false)
        {
            currentDelay -= Time.deltaTime;
            if(currentDelay <= 0)
            {
                canshoot = true;
            }
        }
    }
    public void Shoot()
    { 
        if(canshoot)
        {
                canshoot = false;
                currentDelay = reloadDelay;

                foreach(var barrel in TurretBullets)
                {
                    //GameObject bullet = Instantiate(bulletPrefab);
                    GameObject bullet = bulletPool.CreateObject();

                    bullet.transform.position = barrel.position;
                    bullet.transform.localRotation = barrel.rotation;
                    bullet.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                    bullet.GetComponent<TrailRenderer>().startColor = GetComponent<SpriteRenderer>().color;
                    bullet.GetComponent<TrailRenderer>().endColor = GetComponent<SpriteRenderer>().color;
                    bullet.GetComponent<Bullet>().Initialize();
                    FindObjectOfType<AudioManager>().Play("T_DisparoBala");

                foreach (var collider in tankColliders)
                    {
                        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                    }

                }
        }
    }
    
}
