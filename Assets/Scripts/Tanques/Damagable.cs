using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int MaxHealth = 100;

    [SerializeField] private int health;
    [SerializeField] private GameObject ParticulaExplosion, ParticulaHit;

    private void Start()
    {
        Health = MaxHealth;
    }
    public int Health
    {
        get { return health; }
        set { health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);

        }
    }
    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;


    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if(Health <=0)
        {
            OnDead?.Invoke();
            FindObjectOfType<AudioManager>().Play("T_Explosion");
            Instantiate(ParticulaExplosion, transform.position, Quaternion.identity);
            FindObjectOfType<Shake>().StartShake(2);

        }
        else
        {
            OnHit?.Invoke();
            FindObjectOfType<AudioManager>().Play("T_Hit");
            Instantiate(ParticulaHit, transform.position, Quaternion.identity);
            FindObjectOfType<Shake>().StartShake(0.5f);
        }
    }
    
}
