using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolem : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    [SerializeField] int MoveSpeed;
    [SerializeField] float MaxDist;
    [SerializeField] float MinDist;
    [SerializeField] float attackRange;
    [SerializeField] float projAdj;
    float yLoc;
    public Animator animator;

    public GameObject projectile;
    float timer = 0;
    float delay = 2;
    bool hasFired = false;
    //bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //playerLoc = player.position;
        player = GameObject.FindGameObjectWithTag("Player");
        yLoc = transform.position.y;
    }
    public void TargetAcquire()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Target Acquired");
        Debug.Log(player);
    }
    public void Fire()
    {
        
        
        //Debug.Log("EnemyAttack");
        GameObject projectileHandler;
        Vector3 startPos = transform.position;
        startPos += transform.forward * 3;
        //startPos.x -= .25f;
        startPos.y += 3.5f;
        //startPos.z += 2.5f;
        //startPos.y += projAdj;
        projectileHandler = Instantiate(projectile, startPos, projectile.transform.rotation) as GameObject;

        float projectileSpeed = 25f;
        float projectileHeight = .1f;
        Vector3 aim;
        aim = player.transform.position - transform.position;
        aim *= .1f;
        aim.Normalize();

        
        
        projectileHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(aim.x * projectileSpeed, aim.y * projectileHeight, aim.z * projectileSpeed);
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
            animator.Play("rock pre throw");
            delay = 1.6f;
            hasFired = false;
            timer = 4.0f;
        }
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        if(delay <= 0 && !hasFired)
        {
            Fire();
            hasFired = true;
        }

    }
}
