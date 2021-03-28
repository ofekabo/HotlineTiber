﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] weaponSounds;
    private AudioSource _adSource;
    private int _weaponID;

    private void Start()
    {
        _adSource = GetComponent<AudioSource>();
        GameEvents.events.OnWeaponPickup += PlayGunshotSound;
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
   

}
