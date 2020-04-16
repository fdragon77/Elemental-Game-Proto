using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossManager : MonoBehaviour
{
    [SerializeField] GameObject CrystalMid;
    [SerializeField] GameObject CrystalRight;
    [SerializeField] GameObject CrystalLeft;
    [SerializeField] int explosiveForce;
    [SerializeField] GameObject BrokenCrystal;

    GameObject CurrentBreak;
    int nextBreak = 0;
    List<GameObject> theCrystals = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        theCrystals.Add(CrystalLeft);
        theCrystals.Add(CrystalRight);
        theCrystals.Add(CrystalMid);

        CurrentBreak = theCrystals[nextBreak];

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
        CurrentBreak.SetActive(false);
        Vector3 pos = CurrentBreak.transform.position;
        GameObject Fragments = Instantiate(BrokenCrystal, pos, CurrentBreak.transform.rotation) as GameObject;
        Rigidbody[] theBodies = Fragments.GetComponentsInChildren<Rigidbody>();
        if (theBodies.Length > 0)
        {
            foreach (Rigidbody body in theBodies)
            {
                body.AddExplosionForce(explosiveForce, transform.position, 1);
            }
        }
        nextBreak++;
        if(nextBreak < theCrystals.Count)
        {
            CurrentBreak = theCrystals[nextBreak];
        }
       
    }
}
