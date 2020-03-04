using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBehavior : MonoBehaviour
{
    //We will need a trigger for this eventually. 
    [SerializeField] float max_distance = 50f;
    [SerializeField] float speed = 1;
    [SerializeField] float lifetime = 10f;
    [SerializeField] GameObject theVisual;
    [HideInInspector] GameObject player;
    List<GameObject> theAttacks = new List<GameObject>();
    
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
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            GameObject visualHandler;
            Vector3 startPos = transform.position;
            visualHandler = Instantiate(theVisual, startPos, theVisual.transform.rotation) as GameObject;

            theAttacks.Add(visualHandler);
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                Boom();
            }
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == player.gameObject.tag)
        {
            
            Boom();
        }
    }

    private void Boom()
    {
        Debug.Log(theAttacks.Count);
        foreach (GameObject theObject in theAttacks)
        {
            Debug.Log("Delete Me");
            Destroy(theObject);
        }
        Destroy(gameObject);
    }
}
