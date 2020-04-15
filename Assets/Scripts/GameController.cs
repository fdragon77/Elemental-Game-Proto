using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;
using UnityEngine.Events;
using TMPro;

public class GameController : MonoBehaviour
{
    //Keeps track of the speed of objects in the game. This needs to be used in any scripts with motion.
    public static float gamespeed = 1;
    //SINGLETON IMPLIMENTATION 
    static GameController inst;
    //Delegate to Quit the game.
    UnityAction quitAction;
    //DOES NOTHING.
    UnityAction nullAction;

    private int points = 0;
    [SerializeField] public TextMeshProUGUI PointsDisplay;
    [SerializeField] public GameObject DamageCounter;
    [SerializeField] public GameObject HealFlame;
    public Vector3 LastCheckpoint;
    public int playerHealth = 0;
    // Start is called before the first frame update
    void Awake()
    {
        //SINGLETON IMPLIMENTATION
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
        //Set delegates for set actions. 
        quitAction += quitGame;
        nullAction += DoNothing;
        Screen.SetResolution(1920, 1080, true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            
            if (gamespeed > 0)
            {
                gamespeed = 0;
                Debug.Log("SPEED: " + gamespeed.ToString());
            }
            else
            {
                gamespeed = 1;
                Debug.Log("SPEED: " + gamespeed.ToString());
            }
        }
    }
    /// <summary>
    /// Loads given scene by string.
    /// </summary>
    /// <param name="scn"></param>
    public void loadScene(string scn)
    {
        //Loads given scene.
        SceneManager.LoadScene(scn);
    }

    /// <summary>
    /// Creates a popup to quit the game. 
    /// </summary>
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

    /// <summary>
    /// Closes the application
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Does absolutelty nothing.
    /// </summary>
    public void DoNothing()
    {

    }

    /// <summary>
    /// Sets the speed of the game. 1 for normal speed, 0 for pause. Can be set to a float between for slow motion. 
    /// </summary>
    /// <param name="speed"></param>
    public void setGameSpeed(float speed)
    {
        gamespeed = speed;
    }

    public void addPoints(int p)
    {
        points += p;
        PointsDisplay.text = "Points: " + points.ToString();
    }
}
