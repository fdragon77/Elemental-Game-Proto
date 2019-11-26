using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeUp : MonoBehaviour
{
    LevelingUp lup;
    public GameObject nextStage;
    public GameObject effect;
    GameObject currentPlayer;
    //public Transform oldTarget;
    public CameraFollow cameraTargeter;

    private bool timing = false;
    public float waitTime = 1f;
    private float starttime;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
 
    }
    private void OnTriggerEnter(Collider collision)
    {
        

        if ((collision.gameObject.tag == "Player"))
        {
            Vector3 loc = currentPlayer.transform.position;
            nextStage = Instantiate(nextStage, loc, currentPlayer.transform.rotation) as GameObject;
            cameraTargeter.target= nextStage.transform;
            currentPlayer.SetActive(false);
            gameObject.SetActive(false);
            effect = Instantiate(effect, loc, currentPlayer.transform.rotation);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                if (enemy.GetComponent<BasicEnemy>() != null)
                {
                    enemy.GetComponent<BasicEnemy>().TargetAcquire();
                }
                else if (enemy.GetComponent<ChaserEnemy>() != null)
                {
                    enemy.GetComponent<ChaserEnemy>().TargetAcquire();
                }
                else if (enemy.GetComponent<RockGolem>() != null)
                {
                    enemy.GetComponent<RockGolem>().TargetAcquire();
                }
            }
            lup = nextStage.GetComponent<LevelingUp>();
            lup.popup();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
