using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCleaner : MonoBehaviour
{
    [SerializeField] List<GameObject> checkpoints;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void destroyall()
    {
        foreach(GameObject G in checkpoints)
        {
            Destroy(G);
        }
        Destroy(gameObject);
    }
}
