using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] weaponSounds;
    private AudioSource _adSource;
    private int _weaponID;

    private void Awake()
    {
        _adSource = GetComponent<AudioSource>();
        PlayerWeapons.ChooseWepDel += SetWepID;
        GameEvents.events.OnplayGunshot += PlayGunshotSound;

    }

   
    private void SetWepID(int id)
    {
        _weaponID = id;
    }

    private void PlayGunshotSound()
    {
        if (_weaponID == 1)
        {
            _adSource.PlayOneShot(weaponSounds[0],1f);
        }

        if (_weaponID == 2)
        {
            _adSource.PlayOneShot(weaponSounds[1],1f);
        }

        if (_weaponID == 3)
        {
            _adSource.PlayOneShot(weaponSounds[2],1f);
        }
        
    }
   

}
