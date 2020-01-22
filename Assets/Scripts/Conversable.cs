using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class Conversable : MonoBehaviour
{
    [Header("List of Speaker Names:")]
    [SerializeField] List<string> Speakers;
    [Header("Message text:")]
    [SerializeField] List<string> Lines; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Triggers the message for the given dialogue.
    /// </summary>
    public void Converse()
    {
        for(int i = 0; i < Speakers.Count; i++)
        {
            UIPopup pop = UIPopup.GetPopup("dialogue1");
            pop.Data.SetLabelsTexts(Speakers[i], Lines[i]);
            UIPopupManager.AddToQueue(pop);
        }
    }

    /// <summary>
    /// Used to test use of converse. 
    /// </summary>
    public void Update()
    {
        /*
        //For testing press C to get a default popup.
        if (Input.GetKeyDown(KeyCode.C) && GameController.gamespeed > 0)
        {
            Converse();
        }
        */
    }
}
