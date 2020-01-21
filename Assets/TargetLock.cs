using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] Camera PlayerCam;
    GameObject lockedEnemyOb;
    //Image crosshair;

    bool lockedOn;

    int lockedEnemyList;

    public static List<GameObject> targetableEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //PlayerCam = GameObject.FindObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
