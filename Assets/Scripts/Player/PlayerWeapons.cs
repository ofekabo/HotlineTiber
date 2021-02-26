using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapons : MonoBehaviour
{
    public delegate void ChooseWeaponDelegate(int weaponid);
    public static event ChooseWeaponDelegate ChooseWepDel;

    public int WeaponId;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _ar;
    [SerializeField] GameObject _shotgun;

    void Update()
    {
        WeaponOrder();
        WeaponChange();
    }

    private void WeaponOrder()

    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponId = 1;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponId = 2;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponId = 3;
            ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
        }
    }

    private void WeaponChange()
    {
        switch (WeaponId)
        {
            case 1:

                _pistol.SetActive(true);
                _shotgun.SetActive(false);
                _ar.SetActive(false);

                 break;


            case 2:

                _pistol.SetActive(false);
                _shotgun.SetActive(true);
                _ar.SetActive(false);
                
                break;


            case 3:

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
