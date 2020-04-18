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



    [Header("Boss Attack Objects")]
    [SerializeField] GameObject PrisonObject;
    [SerializeField] GameObject CorruptSpike;
    [SerializeField] GameObject BossDamagingSpike;


    private Destructable[] bindings;
    Destructable CurrentBreakD;
    GameObject CurrentBreak;
    int nextBreak = 0;
    float timer = 0;
    List<GameObject> theCrystals = new List<GameObject>();
    List<Destructable> theCrystalsD = new List<Destructable>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        


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

    private void Imprison()
    {
        Debug.Log("PrisonForm");
        GameObject prisonHandler;
        Vector3 startPos = player.transform.position;
        
        prisonHandler = Instantiate(PrisonObject, startPos, PrisonObject.transform.rotation) as GameObject;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            ModelControl.transform.LookAt(player.transform.position);
            //timer -= Time.deltaTime;
        }
            if (Input.GetKeyDown("space"))
        {
            //CrystalBreak();
            Imprison();
        }
    }

    /// <summary>
    /// This controls the destruction of the crystals that are corrupting the elemental
    /// </summary>
    void CrystalBreak()
    {
        CurrentBreakD.CallDestruct();  
        
        
        nextBreak++;
        if(nextBreak < theCrystalsD.Count)
        {
            CurrentBreakD = theCrystalsD[nextBreak];
            CurrentBreak = theCrystals[nextBreak];
        }
        else
        {
            foreach (Destructable binding in bindings)
            {
                binding.CallDestruct();
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
        Debug.Log("Target Acquired");
        Debug.Log(player);
    }
}
