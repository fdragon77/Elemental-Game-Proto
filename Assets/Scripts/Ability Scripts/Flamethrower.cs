using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flamethrower : MonoBehaviour
{
    public GameObject flamethrower;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;

    public RawImage FlamethrowerCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    AbilityManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        FlamethrowerCooldown = GameObject.Find("FlamethrowerFill").GetComponent<RawImage>();
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }
    public void Fire()
    {
        //Debug.Log("flamethrower");
        if (!active)
        {
            timer = cooldown;
            flamethrower.SetActive(true);
            active = true;
            FlamethrowerCooldown.rectTransform.localScale = Empty;
            theManager.currentMana -= theManager.FlamethrowMana;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            flamethrower.SetActive(false);
            active = false;
            FlamethrowerCooldown.rectTransform.localScale = Full;
        }
    }
    
}
