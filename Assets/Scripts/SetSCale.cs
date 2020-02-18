using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSCale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        transform.localScale = new Vector3(5, 1, 5);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
        if(Input.GetKeyDown("t"))
        {
            transform.localScale = new Vector3(40, 1, 40);
        }
    }
}
