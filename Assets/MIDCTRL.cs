using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDCTRL : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            anim.Play("Fireball");
        }
        if (Input.GetButtonDown("2"))
        {
            anim.Play("AOE");
        }
        if (Input.GetButtonDown("3"))
        {
            anim.Play("Breath");
        }
        if (Input.GetButtonDown("4"))
        {
            anim.Play("Firewall");
        }
    }
}
