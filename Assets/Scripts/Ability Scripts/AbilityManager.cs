using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    //hold the events for each script ability
    public UnityEngine.Events.UnityEvent abil1Trigger;
    public UnityEngine.Events.UnityEvent abil2Trigger;
    public UnityEngine.Events.UnityEvent abil3Trigger;
    public UnityEngine.Events.UnityEvent abil4Trigger;
    public UnityEngine.Events.UnityEvent abil5Trigger;
    public UnityEngine.Events.UnityEvent abil6Trigger;

    //maintain if the ability has been bound
   

    //cooldown timers, not necessarily going to be used
   
    //prevent using multiple abilities at a time
    [SerializeField] float allcool;// = .5f;
    float timer;
    bool fire = true;
    // Start is called before the first frame update
    void Start()
    {
        float timer = allcool;
        //Debug.Log(allcool);
    }
    
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer);
        if(timer <=0 && !fire)
        {
            fire = true;
        }
        //Debug.Log("Update");
        if (Input.GetButtonDown("Fire1"))
        {
            fire = false;
            
            Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("1");
            abil1Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("2") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("2");
            abil2Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("3") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("3");
            abil3Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("4") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("4");
            abil4Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("5") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("5");
            abil5Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("6") && fire)
        {
            fire = false;
            timer = allcool;
            Debug.Log("6");
            abil6Trigger.Invoke();
            //to be bound
        }
    }
}
