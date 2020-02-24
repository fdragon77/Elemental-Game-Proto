using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] bool UseLastHealth = false;
    [SerializeField] public int resetHealth = 100;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ResetLocation(GameObject player)
    {
        player.transform.position = gameObject.transform.position;
    }

    public void ResetScene()
    {
        if (UseLastHealth)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerHealth = resetHealth;
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerHealth = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject[] playersgwa = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ResetLocation(player);
        player.GetComponent<CharacterController>().health = resetHealth;
        //Parent needs to be a checkpoint cleaner.
        GetComponentInParent<CheckpointCleaner>().destroyall();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterController>().LastCheckpoint = this;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LastCheckpoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (UseLastHealth)
            {
                resetHealth = other.gameObject.GetComponent<CharacterController>().health;
            }
        }
    }
}
