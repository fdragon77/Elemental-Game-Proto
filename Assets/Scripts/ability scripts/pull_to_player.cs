using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pull_to_player : MonoBehaviour
{
    [SerializeField] abilities ab;
    [SerializeField] float speed;
    [SerializeField] float range;
    // Update is called once per frame
    void Update()
    {
        if (ab.pull_healing && Vector3.Distance(transform.position, ab.gameObject.transform.position) <= range)
        {
            transform.position = Vector3.MoveTowards(transform.position, ab.gameObject.transform.position, speed * Time.deltaTime);
        }
    }
}
