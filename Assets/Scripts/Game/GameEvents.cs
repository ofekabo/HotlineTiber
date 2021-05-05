using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents events;
  



    private void Awake()
    {
        events = this;
    }

    public event Action OnplayGunshot;
    public void PlayGunshot()
    {
        OnplayGunshot?.Invoke();
    }

    public event Action<int,int> onGunPickupTrigger;

    public void RefillAmmo(int weaponID, int Ammo)
    {
        onGunPickupTrigger?.Invoke(weaponID, Ammo);
    }

    public event Action<int> OnWeaponPickup;
    
    public int PlayGunShotID(int weaponID)
    {
        OnWeaponPickup?.Invoke(weaponID);
        return weaponID;
    }

    public event Action OnGunShotCamera;

    public void PlayCameraShake()
    {
        OnGunShotCamera?.Invoke();
    }

    
    public event Action OnUpdateAmmo;
    public void CallUpdateAmmo()
    {
        OnUpdateAmmo?.Invoke();
    }

    public void CallUpdateAmmoDelayed(float delay)
    {
        Invoke("CallUpdateAmmo",delay);
    }
    
    public event Action PlayImpactSound;

    public void CallImpactSound()
    {
        PlayImpactSound?.Invoke();
    }
    
    public event Action PlayFleshImpactSound;
    public void CallFleshImpactSound()
    {
        PlayFleshImpactSound?.Invoke();
    }
}
