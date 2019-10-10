using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    bool abil1bound = false;
    bool abil2bound = false;
    bool abil3bound = false;
    bool abil4bound = false;
    bool abil5bound = false;

    float abil1cool;
    float abil2cool;
    float abil3cool;
    float abil4cool;
    float abil5cool;

    string keyPress;

    //ReferencedScript abil1;
    //ReferencedScript abil2;
    //ReferencedScript abil3;
    //ReferencedScript abil4;
    //ReferencedScript abil5;


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
        Debug.Log("Update");
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1"))
        {
            Debug.Log("1");
            //abil1
            //to be bound
        }
        if (Input.GetButtonDown("2"))
        {
            Debug.Log("2");
            //abil2
            //to be bound
        }
        if (Input.GetButtonDown("3"))
        {
            Debug.Log("3");
            //abil3
            //to be bound
        }
        if (Input.GetButtonDown("4"))
        {
            Debug.Log("4");
            //abil4
            //to be bound
        }
        if (Input.GetButtonDown("5"))
        {
            Debug.Log("5");
            //abil5
            //to be bound
        }
    }
}
