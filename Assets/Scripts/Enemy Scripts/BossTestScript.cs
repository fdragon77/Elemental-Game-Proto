using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTestScript : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    [SerializeField] int MoveSpeed;
    [SerializeField] float MaxDist;
    [SerializeField] float MinDist;
    [SerializeField] float attackRange;
    [SerializeField] float projAdj;
    float yLoc;

    public GameObject projectile;
    public GameObject bossAttack;
    float timer = 0;
    //bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        //playerLoc = player.position;
        player = GameObject.FindGameObjectWithTag("Player");
        yLoc = transform.position.y;
    }

    public void Fire()
    {
        Debug.Log("EnemyAttack");
        float outcome = Random.Range(-100f, 100f);
        if (outcome <= 0)
        {
            RockThrow();
        }
        else
        {
            BossAttack();
        }
    }
    public void TargetAcquire()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Target Acquired");
        Debug.Log(player);
    }

    private void RockThrow()
    {
        GameObject projectileHandler;
        Vector3 startPos = transform.position;
        startPos.y += projAdj;
        projectileHandler = Instantiate(projectile, startPos, projectile.transform.rotation) as GameObject;

        float projectileSpeed = 30f;
        float projectileHeight = .1f;
        Vector3 aim;
        aim = player.transform.position - transform.position;
        aim *= .1f;
        aim.Normalize();
        projectileHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(aim.x * projectileSpeed, aim.y * projectileHeight, aim.z * projectileSpeed);
    }

    private void BossAttack()
    {
        GameObject attackHandler;
        Vector3 startPos = transform.position;
        startPos.y += projAdj;
        attackHandler = Instantiate(bossAttack, startPos, bossAttack.transform.rotation) as GameObject;

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
