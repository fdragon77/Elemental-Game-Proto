using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonBreak : MonoBehaviour
{
    [SerializeField] GameObject me;
    [SerializeField] Destructable myDestroy;
    [SerializeField] Destructable PrisonPillar;
    [SerializeField] Destructable PrisonPillar1;
    [SerializeField] Destructable PrisonPillar2;
    [SerializeField] Destructable PrisonPillar3;
    [SerializeField] Destructable PrisonPillar4;
    [SerializeField] Destructable PrisonPillar5;
    [SerializeField] Destructable PrisonPillar6;
    [SerializeField] Destructable PrisonPillar7;
    [SerializeField] Destructable PrisonPillar8;
    [SerializeField] Destructable PrisonPillar9;
    [SerializeField] Destructable PrisonPillar10;
    [SerializeField] Destructable PrisonPillar11;
    [SerializeField] Destructable PrisonPillar12;

    [SerializeField] float PrisonHealth = 1f;

    private AbilityManager AM;

    List<Destructable> thePillars = new List<Destructable>();
    // Start is called before the first frame update
    void Start()
    {
        thePillars.Add(myDestroy);
        thePillars.Add(PrisonPillar);
        thePillars.Add(PrisonPillar1);
        thePillars.Add(PrisonPillar2);
        thePillars.Add(PrisonPillar3);
        thePillars.Add(PrisonPillar4);
        thePillars.Add(PrisonPillar5);
        thePillars.Add(PrisonPillar6);
        thePillars.Add(PrisonPillar7);
        thePillars.Add(PrisonPillar8);
        thePillars.Add(PrisonPillar9);
        thePillars.Add(PrisonPillar10);
        thePillars.Add(PrisonPillar11);
        thePillars.Add(PrisonPillar12);
        AM = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<AbilityManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.name)
        {
            case "Fireball(Clone)":
            case "Fireball Variant(Clone)":
                PrisonHealth -= AM.FireballDMG;
                break;
            case "fireCone":
                PrisonHealth -= AM.FlamethrowDMG;
                break;
            case "fire wall(Clone)":
                PrisonHealth -= AM.FirewallDMG;  
                break;
            case "AoeDam":
            case "AoeKnock":
                PrisonHealth -= AM.AoeDMG;                  
                break;

                //FIXME Fill in with prefabs as we work on it.
        }
        if (PrisonHealth <= 0)
        {
            foreach (Destructable toBreak in thePillars)
            {
                if (toBreak != null)
                {
                    toBreak.CallDestruct();
                }

            }
            Destroy(me);
        }
        /*
        if (collision.gameObject.tag == "Attack")
        {
            foreach (Destructable toBreak in thePillars)
            {
                if(toBreak != null)
                {
                    toBreak.CallDestruct();
                }
                
            }
        }*/
       
    }
}
