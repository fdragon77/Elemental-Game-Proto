using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleTorchGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> Group;
    [SerializeField] GameObject toActivate;
    public void check()
    {
        bool allLit = true;
        foreach(GameObject G in Group)
        {
            if (!G.GetComponent<puzzleTorch>().burning)
            {
                allLit = false;
                break;
            }
        }
        if (allLit)
        {
            //Do something here to activate stuff!
            Debug.Log("ITS LIT IN HERE!");
        }
    }
}
