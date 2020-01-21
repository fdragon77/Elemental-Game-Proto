using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInSight : MonoBehaviour
{
    Camera theCamera;
    bool beenAdded = false;
    bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        //theCamera = GameObject.FindObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 EnemyPos = theCamera.WorldToViewportPoint(gameObject.transform.position);

        if (EnemyPos.z > 0 && EnemyPos.x > 0 && EnemyPos.z < 1 && EnemyPos.x < 1)
        {
            visible = true;
        }
        else
            visible = false;
        if(visible && !beenAdded)
        {
            beenAdded = true;
            //add to list TargetLock.nearbyEnemies.add(gameobject);
        }*/
    }
}
