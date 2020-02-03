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

    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

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
    public void RemoveLock(GameObject tbr)
    {
        if (lockedOn && tbr == targetableEnemies[lockedEnemy])
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            lockedOn = false;
            crosshair.enabled = false;
            lockedEnemy = 0;
        }
        targetableEnemies.Remove(tbr);
        

    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("LockOn") && !lockedOn)
        {
            Debug.Log("LockOn");
            Debug.Log("There are " + targetableEnemies.Count + " enemies");
            camera1.SetActive(false);
            camera2.SetActive(true);
            if (targetableEnemies.Count >= 1)
            {
                lockedOn = true;
                crosshair.enabled = true;
                Debug.Log("I am locked on to " + lockedEnemyOb);
                if (!beenLocked || lockedEnemyOb == null)
                {
                    //Lock On To First Enemy In List By Default
                    lockedEnemy = 0;
                    lockedEnemyOb = targetableEnemies[lockedEnemy];
                    beenLocked = true;

                    Debug.Log("I am locked on to " + lockedEnemyOb);
                }               
            }           
        }
        else if (Input.GetButtonDown("LockOn") && lockedOn)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            lockedOn = false;
            crosshair.enabled = false;
            //lockedEnemy = 0;
            //lockedEnemyOb = null;
        }

        if (lockedOn && lockedEnemyOb == null)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
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
