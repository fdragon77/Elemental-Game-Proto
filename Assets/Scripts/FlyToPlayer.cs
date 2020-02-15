using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPlayer : MonoBehaviour
{
    //We will need a trigger for this eventually. 
    [SerializeField] float max_distance = 50f;
    [SerializeField] float speed = 1;
    [HideInInspector] GameObject player;
    [SerializeField] bool active = false;
    [SerializeField] int healAmount = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //Move towards the player at given speed. 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
        }
        if(Vector3.Distance(player.transform.position, transform.position) <= max_distance)
        {
            //Activate once player is within range. Do not deactivate. 
            active = player.GetComponent<Heal>().healing || active;
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
            player.GetComponent<CharacterController>().health += healAmount;
        }
    }
}
