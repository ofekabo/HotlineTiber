using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    private const string HostlerWeapon = "holster_weapon";
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    public RaycastWeapon weapon;
    public Transform weaponSlot;
    private RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    
    public Animator rigController;

    private bool isHolstered;

    private bool _pickupWeapon;

    [Header("Default Weapon")]
    [SerializeField] private RaycastWeapon defWeapon;

    public bool PickupWeapon {get => _pickupWeapon;}
    
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
            
        }
        rigController.SetBool(HostlerWeapon, true);

    }

    // RaycastWeapon GetWeapon(int index)
    // {
    //     if (index < 0 || index >= equipped_weapons.Length) {
    //         return null;
    //     }
    //     return equipped_weapons[index];
    // }

    // Update is called once per frame
    void Update()
    {
        // var weapon = GetWeapon(activeWeaponIndex);
       
        if (weapon)
        {
            isHolstered = rigController.GetBool(HostlerWeapon);
            if (!isHolstered)
            {
                if (Input.GetButton("Fire1"))
                {
                    weapon.StartFiring();
                    GameEvents.events.CallUpdateAmmo();
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    weapon.StopFiring();
                    if (weapon.weaponID == 0)
                    {
                        weapon.PNextFire = Time.time;
                    }
                   
                }
            }
            weapon.UpdateWeapon(Time.deltaTime);

            if (weapon.CurrentAmmo <= 0)
            {
                DropWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            isHolstered = rigController.GetBool(HostlerWeapon);
            rigController.SetBool(HostlerWeapon, !isHolstered);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _pickupWeapon = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            _pickupWeapon = false;
        }
       
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        // int weaponSlotIndex = (int)newWeapon.weaponSlot;
        // var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.transform.SetParent(weaponSlot,false);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
        rigController.Play("equip_" + weapon.weaponName);
        rigController.SetBool(HostlerWeapon, false);
        // equipped_weapons[weaponSlotIndex] = weapon;
        //
        // SetActiveWeapon(newWeapon.weaponSlot);

    
    }
    
        public void DropWeapon()
        {
            if (weapon)
            {
                weapon.transform.SetParent(null);
                weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
                weapon.gameObject.AddComponent<Rigidbody>();
                weapon = null;
            }
        }



        // void SetActiveWeapon(WeaponSlot weaponSlot)
    // {
    //     int holsterIndex = activeWeaponIndex;
    //     int activateIndex = (int)weaponSlot;
    //
    //     if (holsterIndex == activateIndex) {
    //         return;
    //     }
    //
    //     StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    // }

    // IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    // {
    //     yield return StartCoroutine(HolsterWeapon(holsterIndex));
    //     yield return StartCoroutine(ActivateWeapon(activateIndex));
    //     activeWeaponIndex = activateIndex;
    // }
    
    // IEnumerator HolsterWeapon(int index)
    // {
    //     isHolstered = true;
    //     var weapon = GetWeapon(index);
    //     if (weapon) {
    //         rigController.SetBool("Holster_Weapon", true);
    //         do {
    //             yield return new WaitForSeconds(0.05f);
    //         } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    //     }
    //     
    //     
    // }

    // IEnumerator ActivateWeapon(int index)
    // {
    //     var weapon = GetWeapon(index);
    //     if (weapon) {
    //         rigController.SetBool("Holster_Weapon", false);
    //         rigController.Play("equip_" + weapon.weaponName);
    //         do {
    //             yield return new WaitForSeconds(0.05f);
    //         } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    //         isHolstered = false;
    //     }
    // }

    // void SetAnimationDelayed() Invoke(nameof(SetAnimationDelayed), 0.001f);
    // {
    //     animOverride["weapon_anim_empty"] = weapon.weaponAnimation;
    // }
    //
    // [ContextMenu("Save Weapon Pose")]
    // private void SaveWeaponPose()
    // {
    //     GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
    //     recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
    //     recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
    //     recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
    //     recorder.TakeSnapshot(0.0f);
    //     recorder.SaveToClip(weapon.weaponAnimation);
    //     UnityEditor.AssetDatabase.SaveAssets();
    // }
}
