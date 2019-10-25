using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [HideInInspector] public bool healing;
    private float lastTrigger;
    [SerializeField] float waitTime;

    // Start is called before the first frame update
    void Start()
    {

        waitTime = gameObject.GetComponent<AbilityManager>().allcool;
    }
    public void Fire()
    {
        Debug.Log("Heal");
        healing = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time >= lastTrigger + waitTime)
        {
            healing = false;
        }
    }
}