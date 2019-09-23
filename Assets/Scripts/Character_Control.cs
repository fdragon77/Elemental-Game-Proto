using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Control : MonoBehaviour
{
    CharacterController characterController;
    
    public AudioSource soundController;
    public float speed = 6.0f;
    private float initSpeed;
    int time = 0;
    public GameObject projectile;
    //public float gravity = 20.0f; not going to use gravity right now

    private Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        initSpeed = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Jump") && time < 1)
        {
            soundController.Play();
            //Debug.Log("Dash");
            speed *= 4;
            time = 20;
        }
        // Move the controller
        characterController.Move(move * Time.deltaTime * speed);
        if (move != Vector3.zero)
            transform.forward = move;
        if (time > 0)
        {
            time--;
            if (time == 0)
            {
                speed = initSpeed;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject fireballHandler;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            Vector3 worldPos;
            Vector3 fireDirection;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f)) //click hit something
            {
                worldPos = hit.point;
            }
            else //click missed the world
            {
                worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            }
            fireDirection = (worldPos - transform.position);//.normalized;
            fireDirection *= 0.1f; //normalizing it without limiting range to exactly 1;
            fireDirection.y = transform.position.y; //might matter for different height enviornments

            fireballHandler = Instantiate(projectile, transform.position, projectile.transform.rotation) as GameObject;

            int fireballSpeed = 10;
            int fireballHeight = 3;
            fireballHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * fireballSpeed, fireDirection.y * fireballHeight, fireDirection.z * fireballSpeed);
        }
    }
}
