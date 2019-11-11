using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    int MoveSpeed = 3;
    float MaxDist = 30f;
    float MinDist = 5f;
    float attackRange = 10f;
    float yLoc;

    public GameObject projectile;
    float timer = 0;
    //bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        //playerLoc = player.position;
        player = GameObject.Find("ElementalPlayer");
        yLoc = transform.position.y;
    }

    public void Fire()
    {
        Debug.Log("EnemyAttack");
        

        GameObject projectileHandler;      
     
        projectileHandler = Instantiate(projectile, transform.position, projectile.transform.rotation) as GameObject;

        float projectileSpeed = 20f;
        float projectileHeight = .1f;
        //projectileHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * projectileSpeed, fireDirection.y * projectileHeight, fireDirection.z * projectileSpeed);
        




    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        timer -= Time.deltaTime;
        if ((Vector3.Distance(transform.position, player.transform.position) <= MaxDist) && (Vector3.Distance(transform.position, player.transform.position) >= MinDist))
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        
        if (timer <= 0 && (Vector3.Distance(transform.position, player.transform.position) <= attackRange))
        {

            Fire();
            timer = 4.0f;
        }
    }
}

