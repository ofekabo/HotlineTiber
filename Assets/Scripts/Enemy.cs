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
    public virtual void Start()
    {
        health = maxhealth;
    }

    public virtual void Update()
    {
        health = Mathf.Clamp(health,0,maxhealth);
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

    public void RecieveDamage(int damage)
    {
        health -= damage;
        Death();
    }
    

    protected void IDamageable()
    {
        Death();
    }
    private void OnDestroy()
    {
        
    }
}
