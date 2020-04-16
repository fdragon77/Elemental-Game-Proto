using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private SnakeHead Head;
    private float speed;
    //private float rotateSpeed;
    [SerializeField] private GameObject Follow;
    private Vector3 go;
    [SerializeField] private float updateTime = .1f;
    private float startTime;
    [SerializeField] private float socialDistance = 0.5f;
    [SerializeField] private bool isFirstBody = false;

    // Start is called before the first frame update
    public void Start()
    {
        speed = Head.speed;
        //rotateSpeed = 1;
        startTime = Time.time;
        go = Follow.transform.position;
        Head.health += 1;
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameController.gamespeed <= 0)
        {
            return;
        }

        if (!Follow.activeSelf)
        {
            Head.health -= 1;
            gameObject.SetActive(false);
            return;
        }

        if (Vector3.Distance(transform.position, Follow.transform.position) >= socialDistance && Head.isActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, go, speed * Time.deltaTime);
        }
        //Quaternion TargetQ = Quaternion.LookRotation(go - transform.position);
        //transform.localRotation = Quaternion.Lerp(transform.rotation, TargetQ, rotateSpeed * Time.deltaTime);

        if (Time.time >= startTime + updateTime)
        {
            startTime = Time.time;
            go = Follow.transform.position;
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
            Head.health -= 1;
            gameObject.SetActive(false);
        }
    }

}
