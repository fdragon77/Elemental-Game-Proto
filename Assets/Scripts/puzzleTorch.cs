using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructable))]
public class puzzleTorch : MonoBehaviour
{
    [SerializeField] private float burnTime = 1f;
    [SerializeField] private GameObject burnEffect;
    public bool burning = false;
    private float StartBurningTime;
    private puzzleTorchGroup group;
    // Start is called before the first frame update
    void Start()
    {
        StartBurningTime = Time.time;
        group = GetComponentInParent<puzzleTorchGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        burnEffect.SetActive(burning);
        if(Time.time >= StartBurningTime + burnTime)
        {
            burning = false;
        }
    }

    public void burn()
    {
        StartBurningTime = Time.time;
        burning = true;
        group.check();
    }
}
