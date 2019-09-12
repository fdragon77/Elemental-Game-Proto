using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathFire : MonoBehaviour
{
    [SerializeField] fireBreath_stats breath;
    public void cast()
    {
        Vector3 pos = gameObject.transform.position;
        GameObject G = Instantiate(breath.gameObject, pos + (transform.forward * 1), gameObject.transform.rotation);
        G.transform.rotation = gameObject.transform.rotation;
    }
}

