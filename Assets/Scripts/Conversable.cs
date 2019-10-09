using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class Conversable : MonoBehaviour
{
    [SerializeField] List<string> Speakers;
    [SerializeField] List<string> Lines; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Converse()
    {
        for(int i = 0; i < Speakers.Count; i++)
        {
            UIPopup pop = UIPopup.GetPopup("dialogue1");
            pop.Data.SetLabelsTexts(Speakers[i], Lines[i]);
            UIPopupManager.AddToQueue(pop);
        }
    }

    public void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.C) && GameController.gamespeed > 0)
        {
            Converse();
        }
    }
}
