﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    bool hasCollided = false;
    public Rigidbody myBody;
    int timer = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        /*if ((collider.tag == "Player") || collider.tag == "fireball")
        {

        }
        else
        {
            transform.localScale *=3;

        }*/
        if (!hasCollided)
        {
            transform.localScale *= 7;
            myBody.Sleep();
            myBody.isKinematic = true;

            hasCollided = true;
        }
        
        
    }
    void Update()
    {
        if (hasCollided)
        {
            timer--;
            Debug.Log("tick");
        }
        if (timer == 0)
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}