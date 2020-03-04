using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laser;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire()
    {
        Debug.Log("Laser");
        if (!active)
        {
            timer = cooldown;
            laser.SetActive(true);
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
                laser.SetActive(false);
                active = false;
            }
        }
    }
}
