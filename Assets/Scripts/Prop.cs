using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Prop : MonoBehaviour
{
    
    [SerializeField] private float health;

    [SerializeField] private bool Destructable = true;

    [SerializeField] private ParticleSystem propDestroyFX;

    private float _psLifeTime;
    [Header("ExplosiveProp")]
    [SerializeField] bool isExplosiveProp;
    private ExplosiveProp _explosiveProp;
    

    // Start is called before the first frame update
    void Start()
    {
        _psLifeTime = propDestroyFX.main.startLifetimeMultiplier;
        _explosiveProp = GetComponent<ExplosiveProp>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        if (Destructable)
        {
            health -= damage;
        }
        DestroySelf();
      
    }
    public void RecDamage(RaycastWeapon weapon)
    {
        if (Destructable)
        {
            health -= weapon.damage;
            DestroySelf();
        }
        
    }

    private void DestroySelf()
    {
        if (health <= 1)
        {
            ParticleSystem DestructFX = Instantiate(propDestroyFX, transform.position += new Vector3(0, 2f, 0),
                quaternion.Euler(-90, 0, 0));
            Destroy(DestructFX, _psLifeTime);
            Destroy(gameObject);
        }
    }


    public void OnRaycastHit(RaycastWeapon weapon)
    {
        health -= weapon.damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isExplosiveProp)
        {
            _explosiveProp.Explode();
        }
        else
        {
            ParticleSystem DestructFX = Instantiate(propDestroyFX, transform.position += new Vector3(0, 2f, 0),
                quaternion.Euler(-90, 0, 0));
            Destroy(DestructFX, _psLifeTime);
            Destroy(gameObject);
        }
    }

}
