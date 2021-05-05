using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] weaponSounds;
    [SerializeField] AudioClip[] impactPropSounds;
    [SerializeField] AudioClip[] fleshImpactSounds;
    [SerializeField] AudioClip explosionSound;
    private AudioSource _adSource;
    private int _weaponID;

    private void Start()
    {
        _adSource = GetComponent<AudioSource>();
        GameEvents.events.OnWeaponPickup += PlayGunshotSound;
        GameEvents.events.PlayImpactSound += DelayedImpactSound;
        GameEvents.events.PlayFleshImpactSound += DelayedFleshImpactSound;
        GameEvents.events.PlayeExplosionSound += HandleExplosionSound;
    }

    

    private void PlayGunshotSound(int weaponID)
    {
        try
        {
            _adSource.PlayOneShot(weaponSounds[weaponID], 1f);
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("Index of audio out of range weapon ID" + weaponID);
        }
    }
    
    void DelayedImpactSound()
    {
        Invoke(nameof(HandleImpactSound), 0.08f);
    }
    private void HandleImpactSound()
    {
        _adSource.PlayOneShot(impactPropSounds[Random.Range(0,impactPropSounds.Length)]);
    }

    void DelayedFleshImpactSound()
    {
        Invoke(nameof(HandleFleshImpactSound),0.1f);
    }
    private void HandleFleshImpactSound()
    {
        if(fleshImpactSounds.Length > 0)
            _adSource.PlayOneShot(fleshImpactSounds[Random.Range(0,impactPropSounds.Length)], 1);
    }

    private void HandleExplosionSound()
    {
        _adSource.PlayOneShot(explosionSound,0.5f);
    }
}
