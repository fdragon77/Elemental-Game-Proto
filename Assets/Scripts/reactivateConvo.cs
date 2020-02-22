using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactivateConvo : MonoBehaviour
{
    [SerializeField] GameObject obj;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            obj.gameObject.SetActive(true);
        }
    }
}
