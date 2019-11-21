using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This whole thing exists to make up mean up.

public class CharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    float regSpeed = 7f;
    float boost = 30f;
    Vector3 forward, right;
    float timer =2.5f;
    bool dash = false;
    GameController GAME;
    float dash_stick_sens = 0.3f;

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
        timer -= Time.deltaTime;
        if (timer <=0 && dash)
        {
            moveSpeed = regSpeed;
            dash = false;
        }
        //dash with the rjoystick
        if((Input.GetAxis("Dash_H") > dash_stick_sens) || (Input.GetAxis("Dash_H") < -dash_stick_sens) || (Input.GetAxis("Dash_H") > dash_stick_sens) || (Input.GetAxis("Dash_H") < -dash_stick_sens))
        {
            timer = .25f;
            dash = true;
            moveSpeed = boost;

            //Just took all the code from Move() here, pretty jank
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Dash_H") * GameController.gamespeed;
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * -Input.GetAxis("Dash_V") * GameController.gamespeed;

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;

            return;
        }
        //if (Input.GetKey("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d"))
        //move with ljoystick or arrows
        if((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) || ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)))
        {
            if(Input.GetButtonDown("Dash"))
            {
                timer = .25f;
                dash = true;
                moveSpeed = boost;
            }
            Move();
        }
        //if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))&& Input.GetKeyUp("space"))
        //if(Input.GetButtonDown("Dash") && (Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical")))
        //{
        //    timer = .25f;
        //    dash = true;
        //    moveSpeed = boost;
        //    Move();  
        //}
    }
    //Correct for isometric camera movements being diagonal; ie: makes up move you up
    void Move()
    { 
        //direction isn't used for anything? Somebody should refactor all this eventually
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal") * GameController.gamespeed;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical") * GameController.gamespeed;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
