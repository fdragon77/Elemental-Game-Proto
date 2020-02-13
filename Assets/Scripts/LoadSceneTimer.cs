using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTimer : MonoBehaviour
{
    private float timer;
    [SerializeField] private float WaitTime = 120;
    [SerializeField] private string scene;
    private bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timer + WaitTime)
        {
            SceneManager.LoadScene(scene);
        }

        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                SceneManager.LoadScene(scene);
            }
            skip = true;
        }
    }
}
