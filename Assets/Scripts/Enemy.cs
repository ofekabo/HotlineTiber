using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public float viewRange;
    public float attackRange;
    private int damageRec;
    public virtual void Start()
    {
        health = maxhealth;
        Bullet.giveDMG += RecieveDamage;
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
            Debug.Log("Im Dead");
        }
    }

    private void RecieveDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        Death();
    }

    private void OnDestroy()
    {
        Bullet.giveDMG -= RecieveDamage;
    }
}
