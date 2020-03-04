using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInSight : MonoBehaviour
{
    Camera theCamera;
    bool beenAdded = false;
    bool visible = true;
    [SerializeField] TargetLock reticle;
    Vector3 distance = new Vector3();
    GameObject thePlayer;
    int range = 150;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<TargetLock>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");

    }
    public void FindPlayer()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }
    public void RemoveThyself()
    {
        if (beenAdded)
        {
            beenAdded = false;
            reticle.RemoveLock(gameObject);
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
        if (GameController.gamespeed > 0)
        {
            distance = thePlayer.transform.position - gameObject.transform.position;
            Debug.Log(gameObject.GetComponentInChildren<Renderer>().isVisible);
            if (gameObject.GetComponentInChildren<Renderer>().isVisible && !beenAdded && distance.x < range && distance.z < range)
            {
                beenAdded = true;
                Debug.Log("I am " + gameObject);
                TargetLock.targetableEnemies.Add(gameObject);
            }
            else if ((!gameObject.GetComponentInChildren<Renderer>().isVisible || distance.x > range || distance.z > range) && beenAdded)
            {

                RemoveThyself();
            }
        }
    }
}
