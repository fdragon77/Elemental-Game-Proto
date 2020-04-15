using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public string toLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            loadScene();
        }
    }

    public void loadScene()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LastCheckpoint = new Vector3(0, 0, 0);
        if (!(SceneManager.GetSceneAt(0) == SceneManager.GetActiveScene()))
        {
            
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerHealth =
                GameObject.FindWithTag("Player").GetComponent<CharacterController>().health;
        }

        SceneManager.LoadScene(toLoad);
        Time.timeScale = 1;
    }
}
