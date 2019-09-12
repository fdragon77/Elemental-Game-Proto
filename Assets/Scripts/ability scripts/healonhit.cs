using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healonhit : MonoBehaviour
{
    [SerializeField] int healAmount;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player_stats>().update_health(healAmount);
            Destroy(gameObject);
        }
    }
}
