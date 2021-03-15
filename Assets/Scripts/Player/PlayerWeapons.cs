using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class PlayerWeapons : MonoBehaviour
{
    private const int Pistol = 1;
    private const int Shotgun = 2;
    private const int AR = 3;

   
    public delegate void ChooseWeaponDelegate(int weaponid);

    public static event ChooseWeaponDelegate ChooseWepDel;

    public int WeaponId;
    [SerializeField] GameObject _pistol;
    
    [SerializeField] GameObject _shotgun;
    [SerializeField] GameObject _ar;

    #region public vars

    public bool shotgunUnlocked = false;
    public bool arUnlocked = false;
    [SerializeField] private Rig pistol;
    [SerializeField] private Rig twoHandedRig;
    
    #endregion

    #region private Refs

    private PlayerController _pc;
    

    #endregion

    private void Start()
    {
        WeaponId = Pistol;
        ChooseWepDel?.Invoke(ReturnWeaponID(WeaponId));
        _pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        WeaponOrder();
        WeaponChange();
        
        if (_pc.isDancing)
        {
            Debug.Log("hello");
            pistol.weight = 0f;
            twoHandedRig.weight = 0f;
            WeaponsFalse();
        }
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
                pistol.weight = 1;
                twoHandedRig.weight = 0;
                break;


            case Shotgun:

                _pistol.SetActive(false);
                _shotgun.SetActive(true);
                _ar.SetActive(false);
                pistol.weight = 0;
                twoHandedRig.weight = 1;

                break;


            case AR:

                _pistol.SetActive(false);
                _shotgun.SetActive(false);
                _ar.SetActive(true);
                pistol.weight = 0;
                twoHandedRig.weight = 1;
                break;
        }
    }

    private int ReturnWeaponID(int Weaponid)
    {
        return Weaponid;
    }

    private void WeaponsFalse()
    {
        _pistol.SetActive(false);
        _shotgun.SetActive(false);
        _ar.SetActive(false);
    }
}