using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void damageHandler(int damage);

    public static event damageHandler giveDMG;
    [SerializeField] int damage;

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        giveDMG?.Invoke(ReturnDamage(damage));
        Destroy(gameObject);
    }

    int ReturnDamage(int damage)
    {
        return damage;
    }
}
