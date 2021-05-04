using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = System.Random;

public class AiWeapons : MonoBehaviour
{
    [Header("Refrences")] private AiAgent _aiAgent;
   
    public Animator rigController;
    public Rig aimingRig;
    public Transform weaponSlot;
    public Transform aiTarget;
    
    [HideInInspector] public RaycastWeapon currentWeapon;
    [HideInInspector] public bool weaponActive = false;
    [HideInInspector] public bool pickedUpWeapon; // not used
    [HideInInspector] public bool hasWeapon;
    
    [Header("Debugging")]
    [Tooltip("Will only be visualized when ai has target")]
    public bool showWeaponRange;
    public Transform meleeShootingPoint;
    [Header("Attack State")]
    [SerializeField][Range(0.02f,0.4f)] float delayTillAttacking;
    public float DelayTillAttacking { get => delayTillAttacking; }
    
    void Awake()
    {
       
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            hasWeapon = true;
            EquipWeapon(existingWeapon);
        }
        if(!existingWeapon)
        {
            hasWeapon = false;
        }
        
    }

    void Start()
    {
        if (currentWeapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
        {
            currentWeapon.shootingPoint = meleeShootingPoint;
        }
    }

    private void Update()
    {
        if (aiTarget && currentWeapon && weaponActive)
        {
            currentWeapon.UpdateWeapon(Time.deltaTime);
            currentWeapon.StartFiring();
            if (currentWeapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
            {
                rigController.Play("hit_" + currentWeapon.weaponName);
            }
        }
        if(currentWeapon)
        {
            currentWeapon.StopFiring();
        }
    }

    

    public void EquipWeapon(RaycastWeapon weapon)
    {
        
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(weaponSlot,false);
        currentWeapon.transform.localPosition = Vector3.zero;
        rigController.Play("hostler_" + weapon.weaponName);
        hasWeapon = true;
        StartCoroutine(SetWeaponRange(weapon));
    }

    IEnumerator SetWeaponRange(RaycastWeapon weapon)
    {
        yield return new WaitForSeconds(0.5f);
        if (weapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
        {
            GetComponent<AiAgent>().navMeshAgent.stoppingDistance = 2f;
        }
        if (weapon.weaponType == RaycastWeapon.WeaponType.RangeWeapon)
        {
            GetComponent<AiAgent>().navMeshAgent.stoppingDistance = 6f;
        }
        

    }

    public void DrawWeapon(bool drawWeapon)
    {
        rigController.SetBool("weaponPicked", drawWeapon);
    }

    public void SetWeightAiming(float weight)
    {
        aimingRig.weight = weight;
    }

    public void DropWeaon()
    {
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon.DissolveAnim.Invoke("DissolveDeath",0.5f);
            Destroy(currentWeapon.gameObject,currentWeapon.DissolveAnim.TweenTime + 0.5f);
            currentWeapon = null;
        }
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    public Transform SetTarget(Transform target)
    {

        aiTarget.position = new Vector3(target.position.x, UnityEngine.Random.Range(1f,1.8f), target.position.z);
        aiTarget.position += UnityEngine.Random.insideUnitSphere * currentWeapon.inAccuracy;
        return aiTarget;
    }
    
}
