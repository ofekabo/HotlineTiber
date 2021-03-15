using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class RadioScript : MonoBehaviour
{

    private AudioSource _as;

    [SerializeField] private LayerMask layer;
    private int _lastBullet;
    private Vector3 _center;
  
    // Start is called before the first frame update
    void Awake()
    {
        _as = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] hits = (Physics.OverlapBox(transform.position, new Vector3(1.5f, 1.5f, 1.5f), transform.rotation));
        foreach (var hit in hits)
        {
            if (hit.gameObject.GetComponent<Bullet>() && hit.GetInstanceID() != _lastBullet)
            {
                
                _lastBullet = hit.GetInstanceID();
                _as.pitch -= 0.1f;
            }

           

        }


    }

    private void OnDrawGizmos()
    {
        _center = transform.GetComponent<Renderer>().bounds.center;
        Gizmos.color = new Vector4(1, 0, 0, 0.3f);
        Gizmos.DrawCube(_center, new Vector3(1.5f, 1.5f, 1.5f));
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
