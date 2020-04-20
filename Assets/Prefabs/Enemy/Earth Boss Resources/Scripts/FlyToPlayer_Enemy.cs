using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayer_Enemy : MonoBehaviour
{
    //We will need a trigger for this eventually. 

    [SerializeField] float speed = 1;
    [HideInInspector] GameObject player;
    [SerializeField] bool active = true;
    [SerializeField] int damageAmount = 5;
    [SerializeField] float timer = 5;
    [SerializeField] Rigidbody myBody;
    bool gravityOn = false;
    Vector3 targetVector = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
           
            if (active && timer >0)
            {
                timer -= Time.deltaTime;
                targetVector.x = player.transform.position.x;
                targetVector.y = player.transform.position.y+2;
                targetVector.z = player.transform.position.z;
                //Move towards the player at given speed. 
                transform.position = Vector3.MoveTowards(transform.position,targetVector, speed * Time.deltaTime);
                Quaternion TargetQ = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = TargetQ;
            }
            else if (!gravityOn)
            {
                gravityOn = true;
                myBody.useGravity = true;
                myBody.isKinematic = false;
            }
           
        }
    }

    /// <summary>
    /// Destroy the object once it collides with the player. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == player.gameObject.tag)
        {
            Destroy(gameObject);
            player.GetComponent<CharacterController>().health -= damageAmount;
        }
    }
}
