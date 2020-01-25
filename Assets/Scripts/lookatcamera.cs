using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour
{
    private GameObject maincam;
    // Start is called before the first frame update
    void Start()
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(transform.position * 2 - maincam.transform.position);
    }
}
