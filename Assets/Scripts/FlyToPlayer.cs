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
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
        if(Vector3.Distance(player.transform.position, transform.position) <= max_distance)
        {
            active = player.GetComponent<Heal>().healing || active;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == player.gameObject.tag)
        {
            Destroy(gameObject);
        }
    }
}
