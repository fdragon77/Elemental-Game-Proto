using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructable))]
public class SnakeHead : MonoBehaviour
{
    private GameObject Player;
    private GameObject Target;

    [SerializeField] public float speed;
    [SerializeField] public float rotateSpeed;
    [SerializeField] public bool isActivated;
    [SerializeField] private float ActivateDistance = 100;

    private Destructable Damagable;
 
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = Player;
        Damagable = gameObject.GetComponent<Destructable>();
        Damagable.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (!isActivated)
            {
                if (Vector3.Distance(transform.position, Player.transform.position) <= ActivateDistance)
                {
                    isActivated = true;
                }
            }
            else
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                Quaternion TargetQ = Quaternion.LookRotation(Target.transform.position - transform.position);
                TargetQ.z = transform.rotation.z;
                transform.localRotation = Quaternion.Lerp(transform.rotation, TargetQ, rotateSpeed * Time.deltaTime);
            }
        }
    }

    public void ItsJustTheHeadNow()
    {
        Damagable.enabled = true;
    }
}
