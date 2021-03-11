using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void damageHandler(int damage);

    public static event damageHandler GiveDMG;
    
    [SerializeField] int damage;
    [SerializeField] private float bulletHeight = 2f;
    [SerializeField] private float bulletSpeed = 80f;
    [SerializeField] private float bulletDetectionRadius = 0.5f;
    private bool _hasHit;


    //testing
    private Vector3 _previousPos;

    // Start is called before the first frame update
    void Start()
    {
        _previousPos = transform.position;
        Destroy(gameObject,4f);
        

    }

    // Update is called once per frame
    void Update()
    {
        _previousPos = transform.position;
        transform.position = new Vector3(transform.position.x, bulletHeight, transform.position.z);
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime,Space.World);
        RaycastHit[] hits= Physics.RaycastAll(new Ray(_previousPos, (transform.position - _previousPos).normalized),
            (transform.position - _previousPos).magnitude);
        
        for (int i = 0; i < hits.Length; i++)
        {
            
            if (hits[i].collider.CompareTag("Prop"))
            {
                hits[i].collider.GetComponent<Rigidbody>().AddForce(transform.forward * 2f, ForceMode.Impulse);
               
            }

            if (hits[i].collider.CompareTag("Enemy"))
            {
                GiveDMG?.Invoke(ReturnDamage(damage));
            }

        }
        
        
        
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, bulletDetectionRadius);
    }



    int ReturnDamage(int damage)
    {
        return damage;
        
    }
    
}
