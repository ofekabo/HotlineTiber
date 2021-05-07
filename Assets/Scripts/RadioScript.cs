using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;

public class RadioScript : MonoBehaviour
{

    private AudioSource _as;
    [SerializeField] GameObject GameVolume;
    [SerializeField] AudioClip[] songs;
    [SerializeField] private LayerMask layer;
    private int _lastBullet;
    private Vector3 _center;
    
    [SerializeField] float skipforwardTime = 4.3f;
    
    int randomClip;

    int newClip;
    

    private void Start()
    {
        _as = GetComponent<AudioSource>();
        ActiveWeapon.OnWeaponDraw += HandleWeaponDraw;
    }

    public void HandleHitRadio()
    {
        randomClip = UnityEngine.Random.Range(0, songs.Length);
        if(randomClip == newClip)
        {
            HandleHitRadio();
        }

        if (randomClip != newClip)
        {
            newClip = randomClip;
            _as.clip = songs[newClip];
            _as.Play();
        }
    }

    void HandleWeaponDraw()
    {
        _as.volume = 0.3f;
        _as.clip = songs[2];
        _as.Play();
        _as.time += skipforwardTime;
        GameVolume.SetActive(true);
    }

// _as.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
    

    private void OnDrawGizmos()
    {
        _center = transform.GetComponent<Renderer>().bounds.center;
        Gizmos.color = new Vector4(1, 0, 0, 0.3f);
        Gizmos.DrawCube(_center, new Vector3(1.5f, 1.5f, 1.5f));
    }

    private void OnDestroy()
    {
        ActiveWeapon.OnWeaponDraw -= HandleWeaponDraw;
    }
}
