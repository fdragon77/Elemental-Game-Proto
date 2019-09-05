using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_around_object : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] float speed;
    [HideInInspector] float vx = 0;
    [HideInInspector] float vy = 0;
    [HideInInspector] float vz = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vx = 0;
        vy = 0;
        vz = 0;

        if (Input.GetKey(KeyCode.A))
        {
            vy += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vy -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            vz += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vz -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            vx += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            vx -= 1;
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(Target.transform);
        transform.RotateAround(Target.transform.position, new Vector3(0, vy, vz), speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, vx * speed * Time.deltaTime);
    }
}
