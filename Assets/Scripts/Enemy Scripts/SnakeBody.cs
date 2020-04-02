using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private SnakeHead Head;
    private float speed;
    private float rotateSpeed;
    [SerializeField] private GameObject Follow;
    private Vector3 go;
    [SerializeField] private float updatetime = .1f;
    private float startTime;
    [SerializeField] private float socialDistance = 0.5f;
    [SerializeField] private bool isFirstBody = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Head.speed;
        rotateSpeed = 1;
        startTime = Time.time;
        go = Follow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (Follow == null)
            {
                Destroy(gameObject);
                return;
            }
            if (Vector3.Distance(transform.position, Follow.transform.position) >= socialDistance && Head.isActivated)
            {
                transform.position = Vector3.MoveTowards(transform.position, go, speed * Time.deltaTime);
            }
            //Quaternion TargetQ = Quaternion.LookRotation(go - transform.position);
            //transform.localRotation = Quaternion.Lerp(transform.rotation, TargetQ, rotateSpeed * Time.deltaTime);

            if (Time.time >= startTime + updatetime)
            {
                startTime = Time.time;
                go = Follow.transform.position;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Head")
        {
            if (isFirstBody)
            {
                Head.ItsJustTheHeadNow();
            }
            Destroy(gameObject);
        }
    }
}
