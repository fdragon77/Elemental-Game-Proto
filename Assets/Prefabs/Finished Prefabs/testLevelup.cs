using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLevelup : MonoBehaviour
{
    LevelingUp lup;
    // Start is called before the first frame update
    void Start()
    {
        lup = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelingUp>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            //lup.fireballupgrade(LevelingUp.upgrade.a);
            lup.popup();
        }
    }
}
