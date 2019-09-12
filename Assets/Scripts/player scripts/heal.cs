using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    [SerializeField] float waittime;
    [HideInInspector] float timeCast;
    [HideInInspector] abilities ab;

    public void Start()
    {
        ab = gameObject.GetComponent<abilities>();
    }
    public void cast()
    {
        ab.pull_healing = true;
        timeCast = Time.time;
    }

    public void FixedUpdate()
    {
        if(Time.time - timeCast > waittime && ab.pull_healing)
        {
            ab.pull_healing = false;
        }
    }

}
