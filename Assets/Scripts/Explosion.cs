using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    bool hasCollided = false;
    public Rigidbody myBody;
    public GameObject theExplosion;
    public SphereCollider trigger;
    public SphereCollider rigid;
    //var renderer = GetComponent("mesh renderer");
    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        /*if ((collider.tag == "Player") || collider.tag == "fireball")
        {

        }
        else
        {
            transform.localScale *=3;

        }*/
        if (!hasCollided)
        {
            transform.localScale *= 7;
            myBody.Sleep();
            myBody.isKinematic = true;
            theExplosion.SetActive(true);
            //renderer.SetActive(false);

            hasCollided = true;
        }
        
        
    }
    void Update()
    {
        if (hasCollided)
        {
            timer++;
            if (timer == 40)
            {
                trigger.enabled = false;
                rigid.enabled = false;
            }
            //Debug.Log("tick");
        }
        if (timer == 200)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}
