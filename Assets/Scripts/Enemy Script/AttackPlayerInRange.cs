using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerInRange : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float range;
    [SerializeField] float speed;
    [HideInInspector] bool pushed = false;
    [HideInInspector] float waitTime = 1;
    [HideInInspector] float timecast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            transform.LookAt(target.transform);
            if (!pushed)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime * -10);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            
        }
        if(Time.time - timecast >= waitTime)
        {
            pushed = true;
        }
    }

    public void push()
    {
        pushed = true;
        timecast = Time.time;
    }
}
