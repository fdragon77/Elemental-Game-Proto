using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavFollow : MonoBehaviour
{
    private NavMeshAgent Navigator;
    private Transform thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Navigator.SetDestination(thePlayer.position);
    }
}


