using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingEnviroment : MonoBehaviour
{
    [SerializeField] Vector3 Goal;
    [SerializeField] Vector3 ResetZone;
    [SerializeField] float speed = 3;
    [SerializeField] float tollerance = 0.1f;
    [SerializeField] bool wrap = true; //true to warp back to start, false to start moving back to start. 

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Goal, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, Goal) <= tollerance && wrap)
        {
            transform.position = ResetZone;
        }
        else if (Vector3.Distance(transform.position, Goal) <= tollerance && !wrap)
        {
            Vector3 temp = ResetZone;
            ResetZone = Goal;
            Goal = temp;
        }
    }
}
