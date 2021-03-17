using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
  public AiHealth aiHealth;
  
  public void ONSphereCastHit(Bullet bullet,Vector3 direction)
  {
      aiHealth.TakeDamage(bullet.damage,direction); 
  }
}
