using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
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
        
        if (!active)
        {
            timer = cooldown;
            Debug.Log("Heal");
            active = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            active = false;
        }
    }
}
