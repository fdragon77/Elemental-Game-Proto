using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleeffeectfollow : MonoBehaviour
{
    public GameObject followingObject;
    public GameObject leadingObject;

    private void Update()
    {
        followingObject.transform.position = leadingObject.transform.position + new Vector3(0f, 30f, 0f);
        // Start is called before the first frame update
    }


}
