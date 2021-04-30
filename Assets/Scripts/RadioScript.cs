using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class RadioScript : MonoBehaviour
{

    private AudioSource _as;
    

    [SerializeField] AudioClip[] songs;
    [SerializeField] private LayerMask layer;
    private int _lastBullet;
    private Vector3 _center;
  
    // Start is called before the first frame update
    void Awake()
    {
        _as = GetComponent<AudioSource>();
        
    }

    public void HandleHitRadio()
    {
        _as.clip = songs[UnityEngine.Random.Range(0,songs.Length)];
        _as.Play();
        _as.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
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
