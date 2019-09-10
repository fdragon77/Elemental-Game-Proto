using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerInRange : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float range;
    [SerializeField] float speed;
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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
