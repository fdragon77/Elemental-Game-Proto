using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static float gamespeed = 1;
    static GameController inst;
    UnityAction quitAction;
    UnityAction nullAction;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        quitAction += quitGame;
        nullAction += DoNothing;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string scn)
    {
        SceneManager.LoadScene(scn);
    }

    public void QuitGamePopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("Popup2");

        if(q_popup is null)
        {
            return;
        }
        q_popup.Data.SetLabelsTexts("Quit Game?", "Are you sure you want to quit?");
        q_popup.Data.SetButtonsLabels("OK", "Cancel");
        q_popup.Data.SetButtonsCallbacks(quitAction, nullAction);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = true;
        q_popup.HideOnBackButton = true;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void DoNothing()
    {

    }

    public void setGameSpeed(float speed)
    {
        gamespeed = speed;
    }
}
