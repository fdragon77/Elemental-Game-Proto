using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossManager : MonoBehaviour
{

    GameObject player;
    Transform playerLoc;


    [Header("Boss Pieces")]
    [SerializeField] GameObject CrystalMid;
    [SerializeField] GameObject CrystalRight;
    [SerializeField] GameObject CrystalLeft;

    [SerializeField] Destructable CrystalMidD;
    [SerializeField] Destructable CrystalRightD;
    [SerializeField] Destructable CrystalLeftD;
    [SerializeField] int explosiveForce;
    [SerializeField] GameObject BrokenCrystal;
    [SerializeField] GameObject BindingArray;

    [SerializeField] GameObject ModelControl;
    [SerializeField] Animator EarthBossAnim;

    [SerializeField] GameObject CorruptOrb;


   [Header("Boss Attack Objects")]
    [SerializeField] GameObject PrisonObject;
    [SerializeField] GameObject BossDamagingSpike;
    [SerializeField] GameObject PlayerDamagingSpike;

    int determinePure = 3;
    int theSpike = 1;
    bool hostile = true;
    int nextAttack = 1;
    private Destructable[] bindings;
    Destructable CurrentBreakD;
    GameObject CurrentBreak;
    int nextBreak = 0;
    [SerializeField] float attackDelay = 5;
    float attackDelayReset;
    [SerializeField] float breakDelay = 5;
    float breakDelayReset;
    bool hasBrokenRecently = false;
    List<GameObject> theCrystals = new List<GameObject>();
    List<Destructable> theCrystalsD = new List<Destructable>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackDelayReset = attackDelay;
        breakDelayReset = breakDelay;

        theCrystalsD.Add(CrystalLeftD);
        theCrystalsD.Add(CrystalRightD);
        theCrystalsD.Add(CrystalMidD);

        theCrystals.Add(CrystalLeft);
        theCrystals.Add(CrystalRight);
        theCrystals.Add(CrystalMid);

        CurrentBreakD = theCrystalsD[nextBreak];
        CurrentBreak = theCrystals[nextBreak];


        bindings = BindingArray.GetComponentsInChildren<Destructable>();
    }

    public void CallImprison()
    {
        Imprison();
    }
    public void CallSpikeLaunch()
    {
        SpikeLaunch();
    }

    public void PureRoll()
    {
        determinePure = Random.Range(1, 6);

    }
    private void Imprison()
    {
        //Debug.Log("PrisonForm");
        GameObject prisonHandler;
        Vector3 startPos = player.transform.position;
        
        prisonHandler = Instantiate(PrisonObject, startPos, PrisonObject.transform.rotation) as GameObject;

        
    }

    private void SpikeLaunch()
    {
        //Debug.Log("SpikeForm");
        GameObject DamageSpikeHandler;
        Vector3 startPos = CorruptOrb.transform.position;
        if(theSpike != determinePure)
        {
            DamageSpikeHandler = Instantiate(PlayerDamagingSpike, startPos, PlayerDamagingSpike.transform.rotation) as GameObject;
            theSpike++;
        }
        else
        {
            DamageSpikeHandler = Instantiate(BossDamagingSpike, startPos, BossDamagingSpike.transform.rotation) as GameObject;
            theSpike++;
        }
        if (theSpike >= 6)
        {
            theSpike = 1;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            ModelControl.transform.LookAt(player.transform.position);
            
            if(attackDelay <= 0 && hostile)
            {
                nextAttack = Random.Range(1, 3);
                switch (nextAttack)
                {
                    case 1:
                        EarthBossAnim.Play("PrisonAttack");
                        //play animation
                        break;
                    case 2:
                        EarthBossAnim.Play("PiercingCrystal");
                        break;
                }
                attackDelay = breakDelayReset; 
            }
            if(breakDelay <= 0)
            {
                hasBrokenRecently = false;
                breakDelay = breakDelayReset;
            }
            attackDelay -= Time.deltaTime;
            breakDelay -= Time.deltaTime;
        }
    }

    /// <summary>
    /// This controls the destruction of the crystals that are corrupting the elemental
    /// </summary>
    void CrystalBreak()
    {
        if (!hasBrokenRecently)
        {
            hasBrokenRecently = true;
            CurrentBreakD.CallDestruct();


            nextBreak++;
            if (nextBreak < theCrystalsD.Count)
            {
                CurrentBreakD = theCrystalsD[nextBreak];
                CurrentBreak = theCrystals[nextBreak];
            }
            else
            {
                hostile = false;
                foreach (Destructable binding in bindings)
                {
                    binding.CallDestruct();
                }
            }
        }
        
       
    }
    /// <summary>
    /// allows for the targeting script on the shard to access which crystal it needs to target
    /// </summary>
    /// <returns></returns>
    public GameObject ActiveCrystal()
    {
        return CurrentBreak;
    }

    /// <summary>
    /// allows the shard to break the crystal 
    /// </summary>
    public void BreakCrystal()
    {
        CrystalBreak();
    }
    /// <summary>
    /// makes sure the boss can reacquire the player if necessary
    /// </summary>
    public void TargetAcquire()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("Target Acquired");
        //Debug.Log(player);
    }
}
