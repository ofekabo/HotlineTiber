using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] private float bulletHeight = 2f;
    [SerializeField] private float bulletSpeed = 80f;
    [SerializeField] private float bulletDetectionRadius = 0.5f;


    //bullet raycasting
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
        RaycastHit[] hits= Physics.SphereCastAll(new Ray(_previousPos, (transform.position - _previousPos).normalized),bulletDetectionRadius,
            (transform.position - _previousPos).magnitude);

        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.CompareTag("Prop"))
            {
                hit.collider.GetComponent<Rigidbody>().AddForce(transform.forward * 2f, ForceMode.Impulse);
                Destroy(gameObject,UnityEngine.Random.Range(0.3f,0.9f));
            }
               
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().RecieveDamage(damage);
                Destroy(gameObject);
            }
                
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, bulletDetectionRadius);
    }
    
}
