using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laser;
    float timer = 0;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire()
    {
        Debug.Log("Laser");
        
        timer = 2.5f;
        laser.SetActive(true);
        active = true;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            laser.SetActive(false);
            active = false;
        }
    }
}
