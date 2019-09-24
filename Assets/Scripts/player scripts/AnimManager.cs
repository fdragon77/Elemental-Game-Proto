using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    private Animator theAnim;
    // Start is called before the first frame update
    void Start()
    {
        theAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
           // Debug.Log("Swing");
            theAnim.SetTrigger("Mouse Click");
        }
    }
}
