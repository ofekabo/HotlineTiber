using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    [Header("CameraShake")] 
    [SerializeField] float duration;
    [SerializeField] float magnitude;
    
    // Start is called before the first frame update
    void Start()
    {
        Weapon.activateCameraShake += StartShake;
    }


    
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;
 
        while(elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
 
            transform.localPosition = new Vector3(x, y,originalPosition.z);
            elapsedTime += Time.deltaTime;
 
            yield return null;
        }
 
        transform.localPosition = originalPosition;
    }

    void StartShake()
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}
