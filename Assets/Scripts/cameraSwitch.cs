using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject onCamera;
    [SerializeField] GameObject offCamera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            onCamera.SetActive(true);
            offCamera.SetActive(false);
        }
    }

}
