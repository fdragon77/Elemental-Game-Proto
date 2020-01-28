using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanMOVE : MonoBehaviour
{
    public Transform[] target;
    public Transform dirTarget;
    public float speed;
    private int current;


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = dirTarget.position - transform.position ;
        Quaternion rotation = Quaternion.LookRotation(direction );
        transform.rotation = rotation  ;
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % target.Length;

        }
    

}
