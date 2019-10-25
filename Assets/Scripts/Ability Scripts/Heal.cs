using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
<<<<<<< HEAD
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
=======
    [HideInInspector] public bool healing;
    private float lastTrigger;
    private float waitTime;

>>>>>>> master
    // Start is called before the first frame update
    void Start()
    {
        waitTime = gameObject.GetComponent<AbilityManager>().allcool;
    }
    public void Fire()
    {
<<<<<<< HEAD
        
        if (!active)
        {
            timer = cooldown;
            Debug.Log("Heal");
            active = true;
        }
=======
        Debug.Log("Heal");
        healing = true;
>>>>>>> master
    }
    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            active = false;
=======
        if(Time.time >= lastTrigger + waitTime)
        {
            healing = false;
>>>>>>> master
        }
    }
}
