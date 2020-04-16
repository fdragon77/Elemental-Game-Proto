using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureShard : MonoBehaviour
{
    //We will need a trigger for this eventually. 
    
    [SerializeField] float speed = 1;
    [SerializeField] EarthBossManager theManager;
    

    GameObject CurrentCrystal;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCrystal = theManager.ActiveCrystal();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            //Move towards the crystal at given speed. 
            transform.position = Vector3.MoveTowards(transform.position, CurrentCrystal.transform.position, speed * Time.deltaTime);
            
        }
    }

    /// <summary>
    /// Destroy the object once it collides with the crystal to be destroyed. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == CurrentCrystal)
        {
            theManager.BreakCrystal();
            Destroy(gameObject);
        }
    }
}
