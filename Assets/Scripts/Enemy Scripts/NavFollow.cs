using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavFollow : MonoBehaviour
{
    private NavMeshAgent Navigator;
    private Transform thePlayer;
    [SerializeField] float MaxDist = 50;
    [SerializeField] float MinDist = 10;
    // Start is called before the first frame update
    void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TargetAcquire()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        // Debug.Log("Target Acquired");
        // Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, thePlayer.transform.position) <= MaxDist) && (Vector3.Distance(transform.position, thePlayer.transform.position) >= MinDist))
        {
            Navigator.SetDestination(thePlayer.position);
            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        
    }
}


