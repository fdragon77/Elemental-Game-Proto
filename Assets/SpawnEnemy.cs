using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {


        if ((collision.gameObject.tag == "Player"))
        {
            toSpawn.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
