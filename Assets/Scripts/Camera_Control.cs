using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public GameObject player;
    public float x;
    public float y;
    public float z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 temp = player.transform.position;
        temp.x = temp.x - x;
        temp.y = y;
        temp.z = temp.z - z;
        // Assign value to Camera position
        transform.position = temp;
    }
}
