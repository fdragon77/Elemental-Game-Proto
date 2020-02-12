using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A;

    private GameObject currentPlaceableObject;

   
    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            ReleaseIfClicked();
        }
        
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void HandleNewObjectHotkey()
    {
       if (Input.GetKeyDown(newObjectHotkey))
        {
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }
}
