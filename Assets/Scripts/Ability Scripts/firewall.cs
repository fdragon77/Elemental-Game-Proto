using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class firewall : MonoBehaviour
{
    //sound
    public AudioClip firewallsnd;
    public AudioSource Playersnd;
    [SerializeField] float firewallSpeed = 4f;

    public RawImage firewallCooldown;
    public GameObject projectile;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    AbilityManager theManager;
    Vector4 color;
    Vector4 fadeColor = new Vector4(60, 60, 60, 0);
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        // get audio source
        if (firewallsnd != null)
        {
            Playersnd.clip = firewallsnd;
        }
        firewallCooldown = GameObject.Find("FirewallFill").GetComponent<RawImage>();
        color = firewallCooldown.color;
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }
    /// <summary>
    /// this triggers the fireball.
    /// </summary>
    public void Fire()
    {
        if (!active)
        {
            Debug.Log("Firewall");

            // play sound for firewall
            if (firewallsnd != null && Playersnd != null)
            {
                Playersnd.Play();
                Playersnd.pitch = Random.Range(0.7f, 3f);
            }

            //fireballCooldown.rectTransform.localScale = Empty;
            Debug.Log("Blah");
            GameObject firewallHandler;
            Vector3 fireDirection;

            fireDirection = transform.forward;
            Vector3 center = transform.position;
            center.y += 4;

            firewallHandler = Instantiate(projectile, center+gameObject.transform.forward*10, projectile.transform.rotation);

            float Height = .15f;
            firewallHandler.transform.rotation = transform.rotation;
            firewallHandler.GetComponent<Rigidbody>().velocity = projectile.transform.TransformDirection(fireDirection.x * firewallSpeed, fireDirection.y * Height, fireDirection.z * firewallSpeed);
            timer = 0;
            active = true;
            //theManager.currentMana -= theManager.FirewallMana;
        }
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
        float ratio = timer / cooldown;
        firewallCooldown.rectTransform.localScale = new Vector3(ratio, 1, 1);
        if (theManager.currentMana < theManager.FirewallMana)
        {
            firewallCooldown.color = fadeColor;
        }
        else
        {
            firewallCooldown.color = color;
        }
    }

}
