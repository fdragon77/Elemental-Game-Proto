using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_back : MonoBehaviour
{
    [SerializeField] GameObject particleSystem;
    [SerializeField] float waitTime;
    [HideInInspector] float timeCast;
    [HideInInspector] ParticleSystem ps;
    [HideInInspector] SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        ps = particleSystem.GetComponent<ParticleSystem>();
        col = particleSystem.GetComponent<SphereCollider>();
        timeCast = Time.time - waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeCast > waitTime && ps.isPlaying)
        {
            col.enabled = false;
            ps.Stop();
        }
    }

    public void cast()
    {
        timeCast = Time.time;
        ps.Play();
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<AttackPlayerInRange>().push();
        }
    }
}
