using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallEffect : MonoBehaviour
{
    [SerializeField] GameObject WallObject;
    GameObject Blast;
    // Start is called before the first frame update
    void Start()
    {

        Blast = Instantiate(WallObject, new Vector3(transform.position.x, transform.position.y-4, transform.position.z), WallObject.transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
