using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossManager : MonoBehaviour
{
    [SerializeField] GameObject CrystalMid;
    [SerializeField] GameObject CrystalRight;
    [SerializeField] GameObject CrystalLeft;

    [SerializeField] Destructable CrystalMidD;
    [SerializeField] Destructable CrystalRightD;
    [SerializeField] Destructable CrystalLeftD;
    [SerializeField] int explosiveForce;
    [SerializeField] GameObject BrokenCrystal;
    [SerializeField] GameObject BindingArray;

    private Destructable[] bindings;
    Destructable CurrentBreakD;
    GameObject CurrentBreak;
    int nextBreak = 0;

    List<GameObject> theCrystals = new List<GameObject>();
    List<Destructable> theCrystalsD = new List<Destructable>();
    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && nextBreak <=2)
        {
            CrystalBreak();
        }
    }

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

    public GameObject ActiveCrystal()
    {
        return CurrentBreak;
    }
    public void BreakCrystal()
    {
        CrystalBreak();
    }
}
