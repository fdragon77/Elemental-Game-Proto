using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flamethrower : MonoBehaviour
{
    public GameObject flamethrower;
    float timer = 0;
    float t2 = 0;
    bool active = false;
    [SerializeField] float cooldown;
    [SerializeField] float animationLenth = 1;
    [SerializeField] float slowSpeed = 10;
    private float spd = 0;
    private CharacterController CC;

    public RawImage FlamethrowerCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    AbilityManager theManager;
    Vector4 color;
    Vector4 fadeColor = new Vector4(60, 60, 60, 0);
    // Start is called before the first frame update
    void Start()
    {
        FlamethrowerCooldown = GameObject.Find("FlamethrowerFill").GetComponent<RawImage>();
        color = FlamethrowerCooldown.color;
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        CC = gameObject.GetComponent<CharacterController>();
        spd = CC.regSpeed;
    }
    public void Fire()
    {
        //Debug.Log("flamethrower");
        if (!active)
        {
            timer = cooldown;
            t2 = animationLenth;
            flamethrower.SetActive(true);
            active = true;
            FlamethrowerCooldown.rectTransform.localScale = Empty;
            //theManager.currentMana -= theManager.FlamethrowMana;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        t2 -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            flamethrower.SetActive(false);
            active = false;
            FlamethrowerCooldown.rectTransform.localScale = Full;
        }
        if (theManager.currentMana < theManager.FlamethrowMana)
        {
            FlamethrowerCooldown.color = fadeColor;
        }
        else
        {
            FlamethrowerCooldown.color = color;
        }

        if(t2 > 0)
        {
            CC.moveSpeed = slowSpeed;
        }
        else
        {
            CC.moveSpeed = spd;
        }
    }
    
}
