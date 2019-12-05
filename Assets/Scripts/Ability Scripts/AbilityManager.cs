using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{


    

    [Header("UI")]
    public RawImage manabar;
 
    //public RawImage AOECooldown;
    //public RawImage HealCooldown;
    //public RawImage Firebreath;
    [Space]

    //hold the events for each script ability
    [Header("Triggers")]
    [SerializeField] UnityEngine.Events.UnityEvent abil1Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil2Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil3Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil4Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil5Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil6Trigger;

    [SerializeField] float mana;
    [SerializeField] float manaRegenMod;
    [HideInInspector] public float currentMana;

    List<UnityEngine.Events.UnityEvent> theAbilities = new List<UnityEngine.Events.UnityEvent>();
    int activeAbil = 0;

    [Space]
    //mana amounts
    [Header("Mana")]
    [SerializeField] float allcool;// = .5f;
    public float FireballMana;
    public float HealMana;
    public float FlamethrowMana;
    public float AoeMana;
    public float FirewallMana;
    public float abil6mana;
    float timer;
    bool fire = true;
    [Space]

    //Damage amounts
    [Header("Damage")]
    [SerializeField] public float FireballDMG = 1f;
    [SerializeField] public float FlamethrowDMG = 1f;
    [SerializeField] public float AoeDMG = 1f;
    [SerializeField] public float FirewallDMG = 1f;
    [SerializeField] public float abil6DMG = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // get audio source
        Playersnd.clip = fireballsnd;
        theAbilities.Add(abil1Trigger);
        theAbilities.Add(abil2Trigger);
        theAbilities.Add(abil3Trigger);
        theAbilities.Add(abil4Trigger);
        theAbilities.Add(abil5Trigger);
        theAbilities.Add(abil6Trigger);
        float timer = allcool;
        currentMana = mana;
        manabar = GameObject.Find("mana bar fill").GetComponent<RawImage>();
        //Debug.Log(allcool);
    }

    private void updateManabar()
    {
        float ratio = currentMana / mana;
        manabar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    public float Mana()
    {
        return mana;
    }
    public float CurrentMana()
    {
        return currentMana;

    }
    // Update is called once per frame
    void Update()
    {

      


        updateManabar();

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
            theAbilities[activeAbil].Invoke();
            timer = allcool;
            fire = false;
            //Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1"))
        {
            // play sound for fireball

            //Playersnd.Play();
            //Playersnd.pitch = Random.Range(0.7f, 3f);
            //currentMana -= FireballMana;
            fire = false;

            // test code for cool down take out later 
            //fireballCooldown.rectTransform.localScale = new Vector3(0, 1, 1);


            //Debug.Log("1");
            activeAbil = 0;
            //to be bound
        }
        if (Input.GetButtonDown("2") && fire && currentMana >= AoeMana)
        {
            activeAbil = 1;
            //currentMana -= AoeMana;
            //fire = false;
            // test code for cool down take out later 
            //AOECooldown.rectTransform.localScale = new Vector3(0, 1, 1);

            //timer = allcool;
            //Debug.Log("2");
            //abil2Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("3") && fire && currentMana >= FlamethrowMana)
        {
            activeAbil = 2;
            //currentMana -= FlamethrowMana;
            //fire = false;
            //timer = allcool;
            //Debug.Log("3");
            //abil3Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("4") && fire && currentMana >= FirewallMana)
        {
            activeAbil = 3;
            //currentMana -= FirewallMana;
            //fire = false;
            //timer = allcool;
            //Debug.Log("4");
            //abil4Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("5") && fire && currentMana >= HealMana)
        {
            activeAbil = 4;
            //currentMana -= HealMana;
            //fire = false;
            //timer = allcool;
            //Debug.Log("5");
            //abil5Trigger.Invoke();
            //to be bound
        }
        if (Input.GetButtonDown("6") && fire && currentMana >= abil6mana)
        {
            activeAbil = 5;
            //currentMana -= abil6mana;
            //fire = false;
            //timer = allcool;
            //Debug.Log("6");
            //abil6Trigger.Invoke();
            //to be bound
        }
    }
}
