using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
  public Health health;

  public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction,float force) 
  {
      health.TakeDamage(weapon.damage, direction,force);
      
      if(transform.GetComponent<PlayerController>())
        health.SpawnBloodEffect(transform.position + new Vector3(0,2f,0));
      else
      {
          health.SpawnBloodEffect(transform.position);
      }
  }
}
