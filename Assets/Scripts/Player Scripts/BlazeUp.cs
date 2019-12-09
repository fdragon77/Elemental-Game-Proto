using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeUp : MonoBehaviour
{
    //a reference to give the player the ability to upgrade an ability upon transformation.
    LevelingUp lup;
    //reference to new prefab
    public GameObject nextStage;
    //visual effect reference
    public GameObject effect;
    //reference to identify the player
    GameObject currentPlayer;
    //public Transform oldTarget;
    public CameraFollow cameraTargeter;

    // Start is called before the first frame update
    void Start()
    {
        //sets the player reference
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
 
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        //on player collision, deactivate current player and instantiate the next player, then reset anything with a reference to the old player to the new instance.
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
                else if (enemy.GetComponent<NavFollow>() != null)
                {
                    enemy.GetComponent<NavFollow>().TargetAcquire();
                }
                else if (enemy.GetComponent<RockGolem>() != null)
                {
                    enemy.GetComponent<RockGolem>().TargetAcquire();
                }
            }
            armor temp = GameObject.Find("manabar border").GetComponent<armor>(); 
            temp.TargetAcquire();
            lup = nextStage.GetComponent<LevelingUp>();
            lup.popup();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
