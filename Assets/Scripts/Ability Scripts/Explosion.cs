using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject projectile;
    bool hasCollided = false;
    public Rigidbody myBody;
    public GameObject theExplosion;
    public GameObject fireMiddle;
    public SphereCollider trigger;
    public SphereCollider rigid;
    float timer = 2.3f;
    [SerializeField] bool Cluster;
    float fireballSpeed = 1f;
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
        if (!hasCollided && Cluster)
        {
            KaBoom();
        }
        else if (!hasCollided)
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
    void KaBoom()
    {
        List<GameObject> Handlers = new List<GameObject>();
        int x = 0;
        while(x < 9)
        {
            GameObject fireballHandler;
            
            Vector3 center = transform.position;
            center.y += 2;

            fireballHandler = Instantiate(projectile, center, projectile.transform.rotation) as GameObject;
            Handlers.Add(fireballHandler);
            x++;
        }
        x = 0;
        List<float> xDirections = new List<float>{ -10, -10, -10, 0, 0, 0, 10, 10, 10, };
        List<float> zDirections = new List<float>{-10, 10, 0, 10, 0, -10, 10, -10, 0 };
        Vector3 fireDirection;
        
        while (x < 9)
        {
            fireDirection.x = xDirections[x];
            fireDirection.y = 10;
            fireDirection.z = zDirections[x];
            float fireballHeight = .15f;
            Handlers[x].GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * fireballSpeed, fireDirection.y * fireballHeight, fireDirection.z * fireballSpeed);
            x++;
            
        }

        
        
        Boom();
    }
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (GameController.gamespeed > 0)
            {
                if (hasCollided)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 1)
                    {
                        trigger.enabled = false;
                        rigid.enabled = false;
                    }
                    //Debug.Log("tick");
                }
                if (timer <= 0)
                {
                    //Debug.Log("Destroy");
                    Destroy(gameObject);
                }
            }
        }
    }
}