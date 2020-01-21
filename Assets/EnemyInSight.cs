using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInSight : MonoBehaviour
{
    Camera theCamera;
    bool beenAdded = false;
    bool visible = true;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    public void RemoveThyself()
    {
        if (beenAdded)
        {
            TargetLock.targetableEnemies.Remove(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        /*Vector3 EnemyPos = theCamera.WorldToViewportPoint(gameObject.transform.position);

        if (EnemyPos.z > 0 && EnemyPos.x > 0 && EnemyPos.z < 1 && EnemyPos.x < 1)
        {
            visible = true;
            Debug.Log("I see you");
        }
        else
            visible = false;
            */
        if(visible && !beenAdded)
        {
            beenAdded = true;
           
            TargetLock.targetableEnemies.Add(gameObject);
        }
    }
}
