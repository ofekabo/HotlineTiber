using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Vector3 offset;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = target.position - Camera.main.transform.position.normalized;
        // bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
        // foregroundImage.enabled = !isBehind;
        // backgroundImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void SetHealthBarPrecentage(float precentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * precentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
