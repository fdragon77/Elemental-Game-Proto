using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlefollow : MonoBehaviour
{
    public GameObject followingObject;
    public GameObject leadingObject;

    private void Update()
    {
        followingObject.transform.position = leadingObject.transform.position;
        // Start is called before the first frame update
    }
}
