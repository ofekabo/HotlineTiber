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

    // Start is called before the first frame update

    public event Action<int,int> onGunPickupTrigger;

    public void RefillAmmo(int weaponID, int Ammo)
    {
        onGunPickupTrigger?.Invoke(weaponID, Ammo);
    }
}
