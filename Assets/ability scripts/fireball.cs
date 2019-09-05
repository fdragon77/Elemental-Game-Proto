using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] GameObject fbobj;
    public void cast()
    {
        Instantiate(fbobj, gameObject.transform);
    }
}
