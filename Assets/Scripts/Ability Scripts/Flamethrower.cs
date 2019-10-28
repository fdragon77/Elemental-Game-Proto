using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject flamethrower;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void Fire()
    {
        //Debug.Log("flamethrower");
        if (!active)
        {
            timer = cooldown;
            flamethrower.SetActive(true);
            active = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            flamethrower.SetActive(false);
            active = false;
        }
    }
    
}
