using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [] Camera PlayerCam;
    Gameobject lockedEnemyOb;
    Image crosshair;

    bool lockedOn;

    int lockedEnemyList;

    public static list<Gameobject> targetableEnemies = new list<Gameobject>();
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam = GameObject.FindObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
