﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("Andrew_Fire_fx");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Filip's Messing Around Scene");
        }
    }
}
