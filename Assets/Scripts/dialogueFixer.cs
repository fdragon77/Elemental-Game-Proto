using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dialogueFixer : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private UnityEvent hide;
    void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            hide.Invoke();
        }
    }
}
