using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
   [SerializeField] Slider HealthSlider;
   
   [SerializeField] PlayerHealth playerHealthScript;
   
   private void Start()
   {
      HealthSlider.maxValue = playerHealthScript.maxHealth;
      HealthSlider.value = playerHealthScript.maxHealth;
      PlayerHealth.UpdateInterface += HandleHealthSlider;
         
   }

   void HandleHealthSlider()
   {
      HealthSlider.value = playerHealthScript.currentHealth;
   }

 
}
