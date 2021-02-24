using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;

    [SerializeField] private Transform shootingPoint;

    [SerializeField] private float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.keyPressed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fire()
    {
        if (PickUpController.slotFull)
        {
            
            Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
            bulletClone.velocity = transform.forward * bulletSpeed;
        }
        
    }
}
