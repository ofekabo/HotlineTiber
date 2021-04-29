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
      GameEvents.events.OnUpdateAmmo += HandleAmmoText;
      
      try
      {
         Invoke(nameof(HandleAmmoText), 0.1f);
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

   void HandleAmmoText()
   {
      ammoText.text = $"Ammo : {playerWeapons.weapon.CurrentAmmo}";
   }
}
