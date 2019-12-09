using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This whole thing exists to make up mean up.

public class CharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float regSpeed;
    [SerializeField] float boost = 40f;
    Vector3 forward, right;
    public float timer = .5f;
    float holdtimer;
    public float dashTimer = .25f;
    float dashtimerhold;
    bool dash = false;
    GameController GAME;
    float dash_stick_sens = 0.3f;

    public int health = 100;
    //public Text healthText;
    public Sprite armor1;
    Sprite[] armorSprites;


    public RawImage DashCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    float ratio;
    
    // Start is called before the first frame update
    void Start()
    {
        DashCooldown = GameObject.Find("DashFill").GetComponent<RawImage>();
        //LoadAllSprites();
        holdtimer = timer;
        dashtimerhold = dashTimer;
        regSpeed = moveSpeed;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= holdtimer)
            timer += Time.deltaTime;
        if(dashTimer<=dashtimerhold)
            dashTimer += Time.deltaTime;
        if (dashTimer >=dashtimerhold && dash)
        {
            moveSpeed = regSpeed;

        }
        if (timer >= holdtimer)
        {
            dash = false;
        }
        //dash with the rjoystick
        if (((Input.GetAxis("Dash_H") > dash_stick_sens) || (Input.GetAxis("Dash_H") < -dash_stick_sens) || (Input.GetAxis("Dash_H") > dash_stick_sens) || (Input.GetAxis("Dash_H") < -dash_stick_sens)) && !dash)
        {
            timer = 0;
            dashTimer = 0;
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
        if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) || ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)))
        {
            if (Input.GetButtonDown("Dash") && !dash)
            {
                timer = 0;
                dashTimer = 0;
                dash = true;
                moveSpeed = boost;
            }
            Move();
        }

        ratio = timer / holdtimer;
        DashCooldown.rectTransform.localScale = new Vector3(ratio, 1, 1);


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
    void LoadAllSprites()
    {

        armorSprites = Resources.LoadAll<Sprite>("armor_stuff");
        armor1 = armorSprites[100 - health];
        foreach (Sprite s in armorSprites)
        {
            Debug.Log(s.name);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "EnemyAttack")
        {
            health -= 5;
            Debug.Log("Ouch");
            //armor1 = armorSprites[100 - health];
            //this.gameObject.GetComponent<Image>().sprite = (armor1);
        }
    }
}
