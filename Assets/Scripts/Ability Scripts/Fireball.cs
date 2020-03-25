using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Fireball : MonoBehaviour
{
    [SerializeField] Animator animS;
    [SerializeField] Animator animM;
    [SerializeField] Animator animL;

    //sound
    //public AudioClip fireballsnd;
    //public AudioSource Playersnd;

    [SerializeField] TargetLock reticleRef;
    public RawImage fireballCooldown;
    public GameObject projectile;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full= new Vector3(1, 1, 1);
    AbilityManager theManager;
    public bool Multishot;
    [HideInInspector] public float fireballSpeed = 10f;
    int NumShots = 3;
    float shotDelay = .2f;

    Vector4 color;
    Vector4 fadeColor = new Vector4(60, 60, 60, 0);
    public float YOffset = 4;

    CharacterController CC;
    // Start is called before the first frame update
    void Start()
    {
        animS = GetComponent<Animator>();

        timer = cooldown;
        // get audio source
       // Playersnd.clip = fireballsnd;
        fireballCooldown = GameObject.Find("FireballFill").GetComponent<RawImage>();
        color = fireballCooldown.color;
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        reticleRef = GameObject.FindGameObjectWithTag("Reticle").GetComponent<TargetLock>();
        CC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
    /// <summary>
    /// this triggers the fireball.
    /// </summary>
    public void Fire()
    {
        if(!active)
        {
            animS.Play("Fireball", -1, 0f);
            animM.Play("Fireball");
            animL.Play("Fireball");
            if (Multishot)
            {
                StartCoroutine(Repeat());
            }
            fire(false);
            timer = 0;
            active = true;
            //theManager.currentMana -= theManager.FireballMana;
        }

    }
    IEnumerator Repeat()
    {
        
        for (int i = 0; i < NumShots; i++)
        {
            fire(true);
            yield return new WaitForSeconds(shotDelay);
        }
    }
    private void fire(bool deviate)
    {
        //Debug.Log("Fireball");

        // play sound for fireball
        //Playersnd.Play();
       // Playersnd.pitch = Random.Range(0.7f, 3f);

        //fireballCooldown.rectTransform.localScale = Empty;

        GameObject fireballHandler;
        Vector3 fireDirection;
        
        
        Vector3 center = transform.position;
        center.y += YOffset;

        fireballHandler = Instantiate(projectile, center, projectile.transform.rotation) as GameObject;

        if (reticleRef.IsLocked())
        {
            fireDirection = reticleRef.CurrentTarget().transform.position;
            fireDirection = (fireDirection - center);
            fireDirection.Normalize();
        }
        else
        {
            fireDirection = transform.forward;
        }
        if (deviate)
        {
            fireDirection.x+= Random.Range(-.1f, .1f);
            fireDirection.z += Random.Range(-.1f, .1f);
        }
        
        float fireballHeight = .15f;
        Vector3 playermovement = transform.forward * fireballSpeed;
        if (CC.playerMoving())
        {
            playermovement += CC.moveDirection();
        }
        Debug.Log("Fireball velocity: " + (playermovement).ToString());
        //fireballHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * fireballSpeed, fireDirection.y * fireballHeight, fireDirection.z * fireballSpeed) + playermovement;
        if (reticleRef.IsLocked())
        {
            fireballHandler.GetComponent<Rigidbody>().velocity = fireDirection.normalized * playermovement.magnitude;
        }
        else
        {
            fireballHandler.GetComponent<Rigidbody>().velocity = playermovement;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (timer <= cooldown)
            {
                timer += Time.deltaTime;
            }
            if (timer >= cooldown && active)
            {

                //fireballCooldown.rectTransform.localScale = Full;
                active = false;
            }
            float ratio = timer / cooldown;
            fireballCooldown.rectTransform.localScale = new Vector3(ratio, 1, 1);
            if (theManager.currentMana < theManager.FireballMana)
            {
                fireballCooldown.color = fadeColor;
            }
            else
            {
                fireballCooldown.color = color;
            }
        }
    }

}
