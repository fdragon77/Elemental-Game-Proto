using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    int MoveSpeed = 3;
    float MaxDist = 20f;
    float MinDist = 2f;
    float yLoc;
    // Start is called before the first frame update
    void Start()
    {
        //playerLoc = player.position;
        player = GameObject.Find("ElementalPlayer");
        yLoc = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);

        if ((Vector3.Distance(transform.position, player.transform.position) <= MaxDist) && (Vector3.Distance(transform.position, player.transform.position) >= MinDist))
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}

