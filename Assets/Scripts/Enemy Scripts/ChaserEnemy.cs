using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    GameObject player;
    Transform playerLoc;
    [SerializeField] int MoveSpeed;
    [SerializeField] float MaxDist;
    [SerializeField] float MinDist;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TargetAcquire()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        //timer -= Time.deltaTime;
        if ((Vector3.Distance(transform.position, player.transform.position) <= MaxDist) && (Vector3.Distance(transform.position, player.transform.position) >= MinDist))
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
