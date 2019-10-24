using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] GameObject theObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(theObject);
        }
    }
}
