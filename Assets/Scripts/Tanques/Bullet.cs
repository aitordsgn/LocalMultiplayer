using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private int damage = 10;
    [SerializeField] private float maxDistance = 10;

    private Vector2 StartPosition;
    private float conquearedDistance = 0;
    private Rigidbody2D rb2d;
    
    private bool rebotado =false;

    [SerializeField] private GameObject ParticulasRebote;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Initialize();
    }
    

    public void Initialize()
    {
        StartPosition = transform.position;
        rb2d.velocity = transform.up * speed;
    }

    private void Update()
    {
        conquearedDistance = Vector2.Distance(transform.position, StartPosition);
        if(conquearedDistance >= maxDistance)
        {
            DisableObject();
        }
        Debug.Log(rb2d.velocity);

        if (rb2d.velocity.x > 0)
        {
            rb2d.rotation = (Mathf.Atan(rb2d.velocity.y / rb2d.velocity.x) * Mathf.Rad2Deg) - 90;

        }  
        else
        {
            rb2d.rotation = (Mathf.Atan(rb2d.velocity.y / rb2d.velocity.x) * Mathf.Rad2Deg) + 90;

        }
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collider " + collision.name);

        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            collision.GetComponentInParent<Vibracion>().RumblePulse(0.5f, 0.5f, 0.2f, 1);
            damagable.Hit(damage);
        }
        DisableObject();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider " + collision.gameObject.name);

        var damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(damage);
            DisableObject();
        }

        if (rebotado)
        {
            rebotado = false;
            DisableObject();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("T_Rebote");
            Instantiate(ParticulasRebote, transform.position, Quaternion.identity);
            rebotado = true;
        }
    }
   
}
