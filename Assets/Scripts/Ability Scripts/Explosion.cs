using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    bool hasCollided = false;
    public Rigidbody myBody;
    public GameObject theExplosion;
    public GameObject fireMiddle;
    public SphereCollider trigger;
    public SphereCollider rigid;
    float timer = 2.3f;
    //var renderer = GetComponent("mesh renderer");
    
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
            Boom();
        }


    }
    void Boom()
    {
        transform.localScale *= 7;
        myBody.Sleep();
        myBody.isKinematic = true;
        theExplosion.SetActive(true);
        fireMiddle.SetActive(false);
        //Debug.Log("BOOM");
        //renderer.SetActive(false);

        hasCollided = true;
    }
    void Update()
    {
        if (hasCollided)
        {
            timer-= Time.deltaTime;
            if (timer <=1)
            {
                trigger.enabled = false;
                rigid.enabled = false;
            }
            //Debug.Log("tick");
        }
        if (timer <=0)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}