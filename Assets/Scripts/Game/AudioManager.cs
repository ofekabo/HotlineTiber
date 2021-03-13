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
        PlayerWeapons.ChooseWepDel += SetWepID;
        GameEvents.events.OnplayGunshot += PlayGunshotSound;
        _adSource = GetComponent<AudioSource>();

    }

   
    private void SetWepID(int id)
    {
        _weaponID = id;
    }

    private void PlayGunshotSound()
    {
        if (_weaponID == 1)
        {
            _adSource.clip = weaponSounds[0];
        }

        if (_weaponID == 2)
        {
            _adSource.clip = weaponSounds[1];
        }

        if (_weaponID == 3)
        {
            _adSource.clip = weaponSounds[2];
        }

        _adSource.Play();
    }
   

}
