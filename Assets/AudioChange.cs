using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class AudioChange : MonoBehaviour
{

    [SerializeField] AudioSource _as;

    [SerializeField] private LayerMask layer;
    private int _lastBullet;
    // Start is called before the first frame update
    void Start()
    {
        _as.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] hits = (Physics.OverlapBox(transform.position, new Vector3(3, 3, 3), quaternion.identity));
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
        Gizmos.color = new Vector4(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
