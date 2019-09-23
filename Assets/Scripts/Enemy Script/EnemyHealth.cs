using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int healthTotal = 100;
    int timer = 25;
    bool beenHit = false;
    public MeshRenderer firstCube;
    public MeshRenderer secondCube;
    public MeshRenderer thirdCube;
    int timesHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beenHit)
        {
            timer--;
            if (timer <= 0)
            {
                beenHit = false;
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Attack") && !beenHit)
        {
            beenHit = true;
            timer = 25;
            healthTotal -= 25;
            timesHit++;
            if(timesHit == 1)
            {
                firstCube.enabled = false;
            }
            else if (timesHit == 2)
            {
                secondCube.enabled = false;
            }
            else if (timesHit == 3)
            {
                thirdCube.enabled = false;
            }
            else
            {
                if (healthTotal <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
