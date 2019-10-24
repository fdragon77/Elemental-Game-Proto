using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] GameObject theObject;
    [SerializeField] Rigidbody theBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Enter");
        Debug.Log(trigger.gameObject.name);
        if (trigger.gameObject.name == "Aoe")
        {
            Debug.Log("Force");
            Vector3 movedirection = transform.position - trigger.transform.position;
            Debug.Log(movedirection);
            theBody.AddForce(movedirection.normalized * 5000);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
