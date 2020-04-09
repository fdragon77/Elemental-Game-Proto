using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Destructable))]
public class snakeTorches : MonoBehaviour
{
    [SerializeField] private float burnTime = 5f;
    [SerializeField] private GameObject burnEffect;
    public bool burning = false;
    private float StartBurningTime;
    [SerializeField] private UnityEvent burnEvent;
    [SerializeField] private UnityEvent StopEvent;
    // Start is called before the first frame update
    void Start()
    {
        StartBurningTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            burnEffect.SetActive(burning);
            if (Time.time >= StartBurningTime + burnTime)
            {
                burning = false;
                StopEvent.Invoke();
            }
        }
    }

    public void burn()
    {
        StartBurningTime = Time.time;
        burning = true;
        burnEvent.Invoke();
    }
}