using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMat : MonoBehaviour
{
    public int health;
    private float Touchtimer;
    float timer = 5;
    int interval = 0;

    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        Touchtimer = Time.time;
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
    }
    private void Update()
    {
      
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            
        {
            Touchtimer = Time.time;
            health -= 1;
            if (health <= 10)
            {
                interval += 1;
                rend.sharedMaterial = material[interval]; 
            }
            else if (health<= 0)
            {
                interval += 1;
                rend.sharedMaterial = material[interval];
            }
          
            if (interval > 1)
            {
                Destroy(gameObject);
            }
        }
      
    }
   

}
