using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This whole thing exists to make up mean up.

public class CharacterController : MonoBehaviour
{

   

    [SerializeField] public float moveSpeed;
    public float regSpeed;
    [SerializeField] float boost = 40f;
    Vector3 forward, right;
    public float timer = .5f;
    float holdtimer;
    public float dashTimer = .25f;
    float dashtimerhold;
    bool dash = false;
    GameController GAME;
    float dash_stick_sens = 0.3f;
    public float rotateSpeed = 0.5f;

    public int health = 100;
    //public Text healthText;
    public Sprite armor1;
    Sprite[] armorSprites;
    [HideInInspector] public bool dashLock = false;

    public RawImage DashCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    float ratio;

    private Plane ground;

    private GameObject DamageDisplay;

    //Form change stuff.
    [Header("Form change adjustments")]
    [SerializeField] GameObject LowBlaze;
    [SerializeField] GameObject MidBlaze;
    [SerializeField] GameObject HighBlaze;
    [SerializeField] GameObject LowCam;
    [SerializeField] GameObject MidCam;
    [SerializeField] GameObject HighCam;
    private TargetLock targeter;
    [SerializeField] float highHealth;
    [SerializeField] float LowHealth;
    //Firebreath adjustment
    [Header("Flamethrower adjustments:")]
    [SerializeField] GameObject firebreath;
    [SerializeField] float FTlowy = 1.71f;
    [SerializeField] float FTmidy = 4.21f;
    [SerializeField] float FThighy = 5.33f;

    //Fireball adjustments
    [Header("Fireball height adjustments:")]
    [SerializeField] float FBlow = 4;
    [SerializeField] float FBmid = 8;
    [SerializeField] float FBhigh = 10;
    private Fireball fireballcontroller;

    //Checkpoints.
    public Checkpoint LastCheckpoint;
    
    //Healthbar
    [SerializeField] private RawImage HealthBar;

    // Start is called before the first frame update
    void Start()
    {
       

        GAME = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if(GAME.LastCheckpoint != new Vector3())
        {
            transform.position = GAME.LastCheckpoint;
        }
        if(GAME.playerHealth != 0)
        {
            health = GAME.playerHealth;
        }
        DashCooldown = GameObject.Find("DashFill").GetComponent<RawImage>();
        //LoadAllSprites();
        holdtimer = timer;
        dashtimerhold = dashTimer;
        regSpeed = moveSpeed;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        ground = new Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1));

        DamageDisplay = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().DamageCounter;
        targeter = GameObject.FindGameObjectWithTag("Reticle").GetComponent<TargetLock>();

        //Fireball script reference
        fireballcontroller = gameObject.GetComponent<Fireball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (health > 100)
            {
                health = 100;
            }
            else if (health <= 0)
            {
                death();
            }
            
            HealthBar.rectTransform.localScale = new Vector3(health / 100.0f, 1, 1);
            
            if (timer <= holdtimer)
                timer += Time.deltaTime;
            if (dashTimer <= dashtimerhold)
                dashTimer += Time.deltaTime;
            if (dashTimer >= dashtimerhold && dash)
            {
                moveSpeed = regSpeed;

            }
            if (timer >= holdtimer)
            {
                dash = false;
            }

            //dash with the rjoystick
            if (Input.GetAxis("Dash") > 0 && !dash && !dashLock)
            {
                timer = 0;
                dashTimer = 0;
                dash = true;
                moveSpeed = boost;

                /*
                timer = 0;
                dashTimer = 0;
                dash = true;
                moveSpeed = boost;

                //Just took all the code from Move() here, pretty jank
                Vector3 rightMovement = transform.right * moveSpeed * Time.deltaTime * Input.GetAxis("Dash_H") * GameController.gamespeed * rotateSpeed;
                Vector3 upMovement = transform.forward * moveSpeed * Time.deltaTime * -Input.GetAxis("Dash_V") * GameController.gamespeed;

                Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

                transform.forward = heading;
                transform.position += rightMovement;
                transform.position += upMovement;

                return;
                */
                Move();
            }
            //if (Input.GetKey("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d"))
            //move with ljoystick or arrows
            if (Input.GetButtonDown("Vertical") || Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetButtonDown("Dash") && !dash && !dashLock)
                {
                    timer = 0;
                    dashTimer = 0;
                    dash = true;
                    moveSpeed = boost;
                }
                Move();
            }
            if (Input.GetButtonDown("Horizontal") || (Input.GetAxis("Horizontal") != 0))
            {
                transform.position += Camera.main.transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
                //transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
            }

            ratio = timer / holdtimer;
            DashCooldown.rectTransform.localScale = new Vector3(ratio, 1, 1);

            //Health/form change stuff.
            if (health >= highHealth)
            {
                //form change stuff.
                HighBlaze.SetActive(true);
                MidBlaze.SetActive(false);
                LowBlaze.SetActive(false);
                //Debug.Log("Camera2 is null?: " + (targeter.camera2 == null).ToString());
                targeter.camera2 = HighCam;

                //flamethrower height:
                firebreath.transform.localPosition = new Vector3(0, FThighy, 1.33f);

                //fireball adjustment
                fireballcontroller.YOffset = FBhigh;
            }
            else if (health >= LowHealth)
            {
                //form change stuff.
                HighBlaze.SetActive(false);
                MidBlaze.SetActive(true);
                LowBlaze.SetActive(false);
                //Debug.Log("Camera2 is null?: " + (targeter.camera2 == null).ToString());
                targeter.camera2 = MidCam;

                //flamethrower height:
                firebreath.transform.localPosition = new Vector3(0, FTmidy, 1.33f);

                //fireball adjustment
                fireballcontroller.YOffset = FBmid;
            }
            else
            {
                //form change stuff.
                HighBlaze.SetActive(false);
                MidBlaze.SetActive(false);
                LowBlaze.SetActive(true);
                //Debug.Log("Camera2 is null?: " + (targeter.camera2 == null).ToString());
                targeter.camera2 = LowCam;

                //flamethrower height:
                firebreath.transform.localPosition = new Vector3(0, FTlowy, 1.33f);

                //fireball adjustment
                fireballcontroller.YOffset = FBlow;
            }
        }
    }
    //Correct for isometric camera movements being diagonal; ie: makes up move you up
    void Move()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((Vector3.ProjectOnPlane(Camera.main.transform.forward, ground.normal).normalized * Input.GetAxis("Vertical")) + (Camera.main.transform.right * Input.GetAxis("Horizontal"))), rotateSpeed);
        //transform.rotation = Quaternion.LookRotation((Vector3.ProjectOnPlane(Camera.main.transform.forward, ground.normal).normalized * Input.GetAxis("Vertical")) + (Camera.main.transform.right * Input.GetAxis("Horizontal")));
        //transform.position += Vector3.ProjectOnPlane(Camera.main.transform.forward, ground.normal).normalized * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        /*
        //direction isn't used for anything? Somebody should refactor all this eventually
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = transform.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal") * GameController.gamespeed * rotateSpeed;
        Vector3 upMovement = transform.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical") * GameController.gamespeed;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
        */
    }
    void LoadAllSprites()
    {

        armorSprites = Resources.LoadAll<Sprite>("armor_stuff");
        armor1 = armorSprites[100 - health];
        foreach (Sprite s in armorSprites)
        {
            //Debug.Log(s.name);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision);
        if(collision.gameObject.tag == "EnemyAttack")
        {
            attackdmg dmg = collision.gameObject.GetComponent<attackdmg>();
            int d;
            if (dmg == null)
            {
                d = 5;
            }
            else
            {
                d = dmg.damage;
            }

            health -= d;
            displayDamage(d.ToString());
            Debug.Log("Ouch");
            //armor1 = armorSprites[100 - health];
            //this.gameObject.GetComponent<Image>().sprite = (armor1);
        }
    }

    void displayDamage(string damage)
    {
        GAME.addPoints(int.Parse(damage) * -1);
        GameObject disp = Instantiate(DamageDisplay, transform.position + new Vector3(0, 5, 0), transform.rotation);
        disp.GetComponent<movingEnviroment>().Goal = disp.transform.up * 100;
        disp.GetComponent<TextMeshPro>().text = "-" + damage;
        disp.GetComponent<TextMeshPro>().color = Color.red;
    }

    public bool playerMoving()
    {
        return Input.GetButtonDown("Vertical") || Input.GetAxis("Vertical") != 0 || Input.GetButtonDown("Horizontal") || (Input.GetAxis("Horizontal") != 0) || Input.GetAxis("Dash") > 0 || dash;
    }

    public Vector3 moveDirection()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        if (Input.GetButtonDown("Horizontal") || (Input.GetAxis("Horizontal") != 0))
        {
            dir += Camera.main.transform.right * Input.GetAxis("Horizontal") * moveSpeed * .5f;
        }
        if (Input.GetButtonDown("Vertical") || Input.GetAxis("Vertical") != 0)
        {
            dir += transform.forward * moveSpeed;
        }
        return dir;
    }

    public void death()
    {
        GAME.addPoints(-100);
        LastCheckpoint.ResetScene();
    }
}
