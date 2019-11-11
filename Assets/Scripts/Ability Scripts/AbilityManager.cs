using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    //sound
    public AudioClip fireballsnd;
    public AudioSource Playersnd;



    [Header("UI")]
    public RawImage manabar;
 
    public RawImage AOECooldown;
    public RawImage HealCooldown;
    public RawImage Firebreath;
    [Space]

    //hold the events for each script ability
    [Header("Triggers")]
    [SerializeField] UnityEngine.Events.UnityEvent abil1Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil2Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil3Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil4Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil5Trigger;
    [SerializeField] UnityEngine.Events.UnityEvent abil6Trigger;

    int currentAbil = 0;
    List<UnityEngine.Events.UnityEvent> theAbilities = new List<UnityEngine.Events.UnityEvent>();
    [SerializeField] float mana;
    [SerializeField] float manaRegenMod;
    [HideInInspector] public float currentMana;

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
        theAbilities.Add(abil1Trigger);
        theAbilities.Add(abil2Trigger);
        theAbilities.Add(abil3Trigger);
        theAbilities.Add(abil4Trigger);
        theAbilities.Add(abil5Trigger);
        theAbilities.Add(abil6Trigger);
        // get audio source
        Playersnd.clip = fireballsnd;
      
        float timer = allcool;
        currentMana = mana;
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
            fire = false;
            timer = allcool;
            if (currentAbil == 0)
            {
                Playersnd.Play();
                Playersnd.pitch = Random.Range(0.7f, 3f);
            }
            theAbilities[currentAbil].Invoke();
            
            //Debug.Log("Melee");
            //melee goes here
        }
        if (Input.GetButtonDown("1"))
        {
            currentAbil = 0;
        }
        else if (Input.GetButtonDown("2"))
        {
            
            currentAbil = 1;
        }
        else if (Input.GetButtonDown("3"))
        {
            currentAbil = 2;
        }
        else if (Input.GetButtonDown("4"))
        {
            currentAbil = 3;
        }
        else if (Input.GetButtonDown("5"))
        {
            currentAbil = 4;
        }
        else if (Input.GetButtonDown("6"))
        {
            currentAbil = 5;
        }
    }
}
