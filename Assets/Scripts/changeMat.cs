using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMat : MonoBehaviour
{
    float timer = 5;
    int interval = 0;

    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
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
            interval += 1;
            rend.sharedMaterial = material[interval];
            if (interval > 1)
            {
                Destroy(gameObject);
            }
        }
      
    }
   

}
