using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayer_Pure : MonoBehaviour
{
    //We will need a trigger for this eventually. 
    
    [SerializeField] float speed = 1;
    [HideInInspector] GameObject player;
    [SerializeField] bool active = true;
    [SerializeField] int damageAmount = 1;
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
            if (active)
            {
                //Move towards the player at given speed. 
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
