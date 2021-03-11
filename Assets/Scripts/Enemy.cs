using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public float viewRange;
    public float attackRange;
    private int damageRec;
    public virtual void Start()
    {
        health = maxhealth;
        
        
    }

    public virtual void Update()
    {
        health = Mathf.Clamp(health,0,maxhealth);
        Bullet.GiveDMG += SetDamageRecieved;

        Death();
    }

    public virtual void MoveToPlayer()
    {
        // if within viewRange move to player , if within attack range stop and shoot / melee attack
    }
    
    public void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void RecieveDamage(int damage)
    {
        health -= damage;
    }
    
    private void SetDamageRecieved(int damage) // setting the bullet damage == to damage rec
    {
         damageRec = damage;
    }

    public abstract void OnTriggerEnter(Collider other);
    
    // each enemy must have on trigger enter ,
    // take example from MeleeEnemy.cs and add for each enemy script IDamageable.

    protected void IDamageable()
    {
        RecieveDamage(damageRec);
        Death();
    }
    private void OnDestroy()
    {
        Bullet.GiveDMG -= RecieveDamage;
    }
}
