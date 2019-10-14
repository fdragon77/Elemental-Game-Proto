using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeBlast : MonoBehaviour
{
    public GameObject aoeRing;
    float timer = 0;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Fire()
    {
        Debug.Log("AoeBlast");
        timer = 5;
        aoeRing.SetActive(true);
        active = true;

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
