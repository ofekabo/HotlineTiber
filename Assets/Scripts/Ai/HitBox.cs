using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
  public AiHealth aiHealth;
  
  public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction) 
  {
      aiHealth.TakeDamage(weapon.damage, direction);
  }
}
