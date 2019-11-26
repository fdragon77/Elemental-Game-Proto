using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Fireball : MonoBehaviour
{
    //sound
  //public AudioClip fireballsnd;
    //public AudioSource Playersnd;


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
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        // get audio source
       // Playersnd.clip = fireballsnd;
        fireballCooldown = GameObject.Find("FireballFill").GetComponent<RawImage>();
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }
    /// <summary>
    /// this triggers the fireball.
    /// </summary>
    public void Fire()
    {
        if(!active)
        {
            if(Multishot)
            {
                StartCoroutine(Repeat());
            }
            fire(false);
            timer = 0;
            active = true;
            theManager.currentMana -= theManager.FireballMana;
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

        fireDirection = transform.forward;
        Vector3 center = transform.position;
        center.y += 4;

        fireballHandler = Instantiate(projectile, center, projectile.transform.rotation) as GameObject;
        if (deviate)
        {
            fireDirection.x+= Random.Range(-.1f, .1f);
            fireDirection.z += Random.Range(-.1f, .1f);
        }
        float fireballHeight = .15f;
        fireballHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * fireballSpeed, fireDirection.y * fireballHeight, fireDirection.z * fireballSpeed);
        
    }
    // Update is called once per frame
    void Update()
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
        float ratio = timer/cooldown;
        fireballCooldown.rectTransform.localScale= new Vector3(ratio, 1, 1);
    }

}
