using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying : MonoBehaviour
{
    [SerializeField] public float speed = 15;
    [SerializeField] public Vector3 direction;
    [SerializeField] float maxDistance;
    [HideInInspector] Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        if(Vector3.Distance(startpos, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
