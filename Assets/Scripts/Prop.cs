using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Prop : MonoBehaviour
{
    
    [SerializeField] private float health;

    [SerializeField] private bool Destructable = true;

    [SerializeField] private GameObject propDestroyFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecDamage(int damage)
    {
        if (Destructable)
        {
            health -= damage;
            if (health <= 1)
            {
                GameObject DestructFX = Instantiate(propDestroyFX, transform.position, Quaternion.identity);
                Destroy(DestructFX, 0.4f);
                Destroy(gameObject);

            }
        }
        
    }
}
