using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] index;
    private AudioSource _auditt;
    private int weaponid;

    private void Start()
    {
        PlayerWeapons.ChooseWepDel += SetWepid;
        GameEvents.events.OnplayGunshot += PlaySound;
        _auditt = GetComponent<AudioSource>();

    }

   
    private void SetWepid(int id)
    {
        weaponid = id;
    }

    private void PlaySound()
    {
        if (weaponid == 1)
        {
            _auditt.clip = index[0];
        }

        if (weaponid == 2)
        {
            return;
        }

        if (weaponid == 3)
        {
            _auditt.clip = index[1];
        }
        _auditt.Play();
    }
   

}
