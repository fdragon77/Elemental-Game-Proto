using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroytimer : MonoBehaviour
{
    [SerializeField] float countdownTime = 2f;
    private float starttime;
    // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (Time.time >= starttime + countdownTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
