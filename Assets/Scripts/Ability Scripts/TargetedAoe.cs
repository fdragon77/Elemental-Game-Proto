﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedAoe : MonoBehaviour
{
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire()
    {
        Debug.Log("TargetedAoe");
        if (!active)
        {
            timer = cooldown;
            active = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && active)
            {
                active = false;
            }
        }
    }
}
