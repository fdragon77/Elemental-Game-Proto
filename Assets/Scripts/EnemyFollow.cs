using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    int MoveSpeed = 3;
    int MaxDist = 20;
    int MinDist = 5;
    // Start is called before the first frame update
    void Start()
    {
        //playerLoc = player.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }
}
