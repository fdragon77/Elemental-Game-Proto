using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This whole thing exists to make up mean up.

public class CharacterController : MonoBehaviour
{
    float moveSpeed = 7f;

    Vector3 forward, right;

    GameController GAME;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
    }
    //Correct for isometric camera movements being diagonal; ie: makes up move you up
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal") * GameController.gamespeed;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical") * GameController.gamespeed;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
