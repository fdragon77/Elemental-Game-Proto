﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public string toLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {

        
        if ((collision.gameObject.tag == "Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LastCheckpoint = new Vector3(0, 0, 0);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerHealth = collision.gameObject.GetComponent<CharacterController>().health;
            SceneManager.LoadScene(toLoad);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("Andrew_Fire_fx");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Filip's Messing Around Scene");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("StartLevel");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("backupmine");
        }
        */

    }
}
