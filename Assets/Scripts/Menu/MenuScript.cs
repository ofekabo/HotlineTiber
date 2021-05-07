using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    
    [SerializeField] Transform aimObject;
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] Transform effector;
    [SerializeField] GameObject hotkeys;
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimLayerMask))
        {
            aimObject.position = hit.point;
        }
        effector.position = aimObject.position;
    }

    public void ToggleHotkeys()
    {
        hotkeys.SetActive(!hotkeys.activeSelf);
    }
}
