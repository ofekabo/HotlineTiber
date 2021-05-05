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
        GameEvents.events.PlayFleshImpactSound += HandleFleshImpactSound;
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

    
    private void HandleFleshImpactSound()
    {
        if(fleshImpactSounds.Length > 0)
            _adSource.PlayOneShot(fleshImpactSounds[Random.Range(0,impactPropSounds.Length)]);
    }

    private void HandleExplosionSound()
    {
        _adSource.PlayOneShot(explosionSound,0.5f);
    }
}
