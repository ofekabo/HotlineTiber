using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
   [Header(("Health"))]
   [SerializeField] Slider HealthSlider;
   [SerializeField] PlayerHealth playerHealthScript;
   [Header("Ammo")]
   [SerializeField] ActiveWeapon playerWeapons;
   [SerializeField] Text ammoText;


   
   private void Start()
   {
      HealthSlider.maxValue = playerHealthScript.maxHealth;
      HealthSlider.value = playerHealthScript.maxHealth;
      PlayerHealth.UpdateInterface += HandleHealthSlider;
      ActiveWeapon.UpdateFireAmmo += HandleFireWeaponText;
      WeaponPickup.UpdatePickupWeapon += HandleFireWeaponText;
      
      try
      {
         ammoText.text = $"Ammo : {playerWeapons.weapon.CurrentAmmo}";
      }
      catch (NullReferenceException e)
      {
         Debug.Log(e + "setting text too early");
      }
      
   }

   void HandleHealthSlider()
   {
      HealthSlider.value = playerHealthScript.currentHealth;
   }

   void HandleFireWeaponText()
   {
      ammoText.text = $"Ammo : {playerWeapons.weapon.CurrentAmmo}";
   }
}
