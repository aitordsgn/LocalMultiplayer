using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectaColision : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite Sprite;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.sprite = Sprite;
        FindObjectOfType<AudioManager>().Play("C_Cono");

    }
}
