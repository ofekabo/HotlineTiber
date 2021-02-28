using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapons : MonoBehaviour
{
    private const int Pistol = 1;
    private const int Shotgun = 2;
    private const int AR = 3;
    
    public delegate void ChooseWeaponDelegate(int weaponid);
    public static event ChooseWeaponDelegate ChooseWepDel;

    public int WeaponId;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _ar;
    [SerializeField] GameObject _shotgun;

    public bool shotgunUnlocked = false;
    public bool arUnlocked = false;

    private void Start()
    {
      
        WeaponId = 1;
        ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
    }
    
    void Update()
    {
        WeaponOrder();
        WeaponChange();
    }

    private void WeaponOrder()

    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponId = Pistol;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && shotgunUnlocked)
        {
            WeaponId = Shotgun;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && arUnlocked)
        {
            WeaponId = AR;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
        }
    }

    private void WeaponChange()
    {
        switch (WeaponId)
        {
            case Pistol:

                _pistol.SetActive(true);
                _shotgun.SetActive(false);
                _ar.SetActive(false);

                 break;


            case Shotgun:

                _pistol.SetActive(false);
                _shotgun.SetActive(true);
                _ar.SetActive(false);
                
                break;


            case AR:

                _pistol.SetActive(false);
                _shotgun.SetActive(false);
                _ar.SetActive(true);

                break;
        }
    }

    private int ReturnWeaponID(int Weaponid)
    {
       
        return Weaponid;
         
    }
    
}
