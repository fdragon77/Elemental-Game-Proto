﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAbility : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider trigger)
    {
        //Debug.Log("Enter abil");
        //Debug.Log(trigger.gameObject.tag);

        if (trigger.gameObject.tag == "Enemy")
        {
            //Debug.Log("Force abil");
            Vector3 movedirection = transform.position - trigger.transform.position;
            Rigidbody theBody = trigger.GetComponent<Rigidbody>();
            //Debug.Log(theBody.name);
            //Debug.Log(movedirection.normalized);
            theBody.AddForce(movedirection.normalized * -5000);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
