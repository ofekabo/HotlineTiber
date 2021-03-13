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
}
