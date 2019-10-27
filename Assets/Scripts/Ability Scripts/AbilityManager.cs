using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    //hold the events for each script ability
    [SerializeField] UnityEngine.Events.UnityEvent abil1Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil2Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil3Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil4Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil5Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil6Trigger;

    [SerializeField] float mana;
    [SerializeField] float manaRegenMod;
    private float currentMana;

    
    //prevent using multiple abilities at a time
    [SerializeField] float allcool;// = .5f;
    [SerializeField] float abil1mana;
    [SerializeField] float abil2mana;
    [SerializeField] float abil3mana;
    [SerializeField] float abil4mana;
    [SerializeField] float abil5mana;
    [SerializeField] float abil6mana;
    float timer;
    bool fire = true;
    // Start is called before the first frame update
    void Start()
    {
        float timer = allcool;
        currentMana = mana;
        //Debug.Log(allcool);
    }
    
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(currentMana < mana)
        {
            currentMana += manaRegenMod*Time.deltaTime;
            //Debug.Log(currentMana);
        }
        else if(currentMana > mana)
        {
            currentMana = mana;
        }
        //Debug.Log(timer);
        if(timer <=0 && !fire)
        {
            fire = true;
        }
        //Debug.Log("Update");
        if (Input.GetButtonDown("Fire1"))
        {
            //fire = false;
            
            //Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1") && fire && currentMana >= abil1mana)
        {
            currentMana -= abil1mana;
            fire = false;
            timer = allcool;
            //Debug.Log("1");
            abil1Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("2") && fire && currentMana >= abil2mana)
        {
            currentMana -= abil2mana;
            fire = false;
            timer = allcool;
            //Debug.Log("2");
            abil2Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("3") && fire && currentMana >= abil3mana)
        {
            currentMana -= abil3mana;
            fire = false;
            timer = allcool;
            //Debug.Log("3");
            abil3Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("4") && fire && currentMana >= abil4mana)
        {
            currentMana -= abil4mana;
            fire = false;
            timer = allcool;
            //Debug.Log("4");
            abil4Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("5") && fire && currentMana >= abil5mana)
        {
            currentMana -= abil5mana;
            fire = false;
            timer = allcool;
            //Debug.Log("5");
            abil5Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("6") && fire && currentMana >= abil6mana)
        {
            currentMana -= abil6mana;
            fire = false;
            timer = allcool;
            //Debug.Log("6");
            abil6Trigger.Invoke();
            //to be bound
        }
    }
}
