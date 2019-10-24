using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBlast : MonoBehaviour
{
    public GameObject aoeRing;
    [SerializeField] GameObject AoeObject;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    GameObject Blast;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire()
    {
        if (!active)
        {
            Debug.Log("AoeBlast");
            timer = cooldown;
            Blast = Instantiate(AoeObject, transform.position, AoeObject.transform.rotation) as GameObject;
            //aoeRing.SetActive(true);
            active = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            aoeRing.SetActive(false);
            active = false;
        }
    }
}
