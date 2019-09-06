using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] GameObject fbobj;
    public void cast()
    {
        Vector3 pos = gameObject.transform.position;
        GameObject G = Instantiate(fbobj, pos + (transform.forward * 1), gameObject.transform.rotation);
        G.transform.rotation = gameObject.transform.rotation;
    }

    public void Update()
    {
        //for testing.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cast();
        }
    }
}
