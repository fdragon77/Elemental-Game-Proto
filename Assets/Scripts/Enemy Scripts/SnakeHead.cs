using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameObject Player;
    private GameObject Target;

    [SerializeField] public float speed;
    [SerializeField] public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            Quaternion TargetQ = Quaternion.LookRotation(Target.transform.position - transform.position);
            TargetQ.z = transform.rotation.z;
            transform.localRotation = Quaternion.Lerp(transform.rotation, TargetQ, rotateSpeed * Time.deltaTime);
        }
    }
}
