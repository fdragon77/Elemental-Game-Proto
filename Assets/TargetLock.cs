using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLock : MonoBehaviour
{
    [SerializeField] Camera PlayerCam;
    GameObject lockedEnemyOb;
    [SerializeField] Image crosshair;

    bool lockedOn;
    bool beenLocked;
    int lockedEnemy;

    public static List<GameObject> targetableEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lockedOn = false;
        lockedEnemy = 0;
        crosshair.enabled = false;

    }
    public bool IsLocked()
    {
        return lockedOn;
    }
    public GameObject CurrentTarget()
    {
        return lockedEnemyOb;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("LockOn") && !lockedOn)
        {
            Debug.Log("LockOn");
            if (targetableEnemies.Count >= 1)
            {
                lockedOn = true;
                crosshair.enabled = true;
                if (!beenLocked || lockedEnemyOb == null)
                {
                    //Lock On To First Enemy In List By Default
                    lockedEnemy = 0;
                    lockedEnemyOb = targetableEnemies[lockedEnemy];
                    beenLocked = true;
                }               
            }           
        }
        else if (Input.GetButtonDown("LockOn") && lockedOn)
        {
            lockedOn = false;
            crosshair.enabled = false;
            //lockedEnemy = 0;
            //lockedEnemyOb = null;
        }

        if (lockedOn && lockedEnemyOb == null)
        {
            lockedOn = false;
            crosshair.enabled = false;
            targetableEnemies.Remove(lockedEnemyOb);
        }
        if (Input.GetButtonDown("SwitchLeft")&& lockedOn)
        {
            if (lockedEnemy == 0)
            {
                //If End Of List Has Been Reached, Start Over
                /*if(targetableEnemies[targetableEnemies.Count-1] == null)
                {
                    bool valid = false;
                    while (!valid)
                    {
                        if (targetableEnemies[targetableEnemies.Count - 1] == null)
                        {
                            targetableEnemies.Remove(targetableEnemies.Count - 1);
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                    
                }*/
                lockedEnemy = targetableEnemies.Count - 1;
                lockedEnemyOb = targetableEnemies[lockedEnemy];
            }
            else
            {
                //Move To Next Enemy In List
                lockedEnemy--;
                lockedEnemyOb = targetableEnemies[lockedEnemy];
            }
        }
        if (Input.GetButtonDown("SwitchRight") && lockedOn)
        {
            if (lockedEnemy == targetableEnemies.Count - 1)
            {
                //If End Of List Has Been Reached, Start Over
                lockedEnemy = 0;
                lockedEnemyOb = targetableEnemies[lockedEnemy];
            }
            else
            {
                //Move To Next Enemy In List
                lockedEnemy++;
                lockedEnemyOb = targetableEnemies[lockedEnemy];
            }
        }

        if (lockedOn && lockedEnemyOb != null)
        {
            lockedEnemyOb = targetableEnemies[lockedEnemy];

            //Determine Crosshair Location Based On The Current Target
            gameObject.transform.position = PlayerCam.WorldToScreenPoint(lockedEnemyOb.transform.position);

            //Rotate Crosshair
            gameObject.transform.Rotate(new Vector3(0, 0, -1));
        }
        

    }
}
