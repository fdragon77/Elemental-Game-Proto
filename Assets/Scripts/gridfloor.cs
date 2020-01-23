using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridfloor : MonoBehaviour
{
    public GameObject[] prefabtiles;
    public int gridx;
    public int gridz;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();

    }
    void SpawnGrid()
    {
        for (int x = 0; x < gridx; x++)
        {
            for (int z = 0; z < gridz; z++)
            {
                Vector3 spawnPosistion = new Vector3(x * gridSpacingOffset,0,z *gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosistion, Quaternion.identity);

            }
        }
    }

    void PickAndSpawn ( Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, prefabtiles.Length);
        GameObject clone = Instantiate(prefabtiles[randomIndex], positionToSpawn, rotationToSpawn);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
