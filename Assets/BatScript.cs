using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    [SerializeField] private GameObject shootingPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootingPoint.transform.localPosition = new Vector3(0.681f, -1.602f, -1.761f);
        shootingPoint.transform.localRotation = Quaternion.Euler(93.68201f, 0.0f, 0.0f);
    }
}
