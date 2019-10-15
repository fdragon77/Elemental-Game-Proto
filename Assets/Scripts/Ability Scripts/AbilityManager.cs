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
    bool abil1bound = false;
    bool abil2bound = false;
    bool abil3bound = false;
    bool abil4bound = false;
    bool abil5bound = false;
    bool abil6bound = false;

    //cooldown timers, not necessarily going to be used
    float abil1cool;
    float abil2cool;
    float abil3cool;
    float abil4cool;
    float abil5cool;
    float abil6cool;

    //prevent using multiple abilities at a time
    float allcool = 2.5f;
    bool fire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool Bind( int binding, float cooldown)//reference script needed
    {
        bool succesfulBind = false;
        //this will assign values for abilbound and abil cool as well as what abil is bound
        switch (binding)
        {
            case 1:
                {
                    if (!abil1bound)
                    {
                        //abil1 = theAbil;
                        abil1bound = true;
                        succesfulBind = true;
                        abil1cool = cooldown;
                    }
                }
                break;
            case 2:
                {
                    if (!abil2bound)
                    {
                        //abil2 = theAbil;
                        abil2bound = true;
                        succesfulBind = true;
                        abil2cool = cooldown;
                    }

                }
                break;
            case 3:
                {
                    if (!abil3bound)
                    {
                        //abil3 = theAbil;
                        abil3bound = true;
                        succesfulBind = true;
                        abil3cool = cooldown;
                    }
                }
                break;
            case 4:
                {
                    if (!abil4bound)
                    {
                        //abil4 = theAbil;
                        abil4bound = true;
                        succesfulBind = true;
                        abil4cool = cooldown;
                    }
                }
                break;
            case 5:
                {
                    if (!abil5bound)
                    {
                        //abil5 = theAbil;
                        abil5bound = true;
                        succesfulBind = true;
                        abil5cool = cooldown;
                    }
                }
                break;
            case 6:
                {
                    if (!abil6bound)
                    {
                        //abil1 = theAbil;
                        abil6bound = true;
                        succesfulBind = true;
                        abil6cool = cooldown;
                    }
                    break;
                }
            default:
                {
                    Debug.Log("Default case for binding.");
                }
                break;
        }
        return succesfulBind;

    }
    // Update is called once per frame
    void Update()
    {
        allcool -= Time.deltaTime;
        if(allcool <=0 && !fire)
        {
            fire = true;
        }
        //Debug.Log("Update");
        if (Input.GetButtonDown("Fire1"))
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("1");
            abil1Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("2") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("2");
            abil2Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("3") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("3");
            abil3Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("4") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("4");
            abil4Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("5") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("5");
            abil5Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("6") && fire)
        {
            fire = false;
            allcool = 2.5f;
            Debug.Log("6");
            abil6Trigger.Invoke();
            //to be bound
        }
    }
}
