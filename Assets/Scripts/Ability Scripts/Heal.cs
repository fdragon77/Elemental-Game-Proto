using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    [HideInInspector] public bool healing;
    private float lastTrigger;
    [SerializeField] float waitTime;

    public RawImage HealCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    AbilityManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        //HealCooldown = GameObject.Find("HealFill").GetComponent<RawImage>();
        //waitTime = gameObject.GetComponent<AbilityManager>().allcool;
    }
    public void Fire()
    {
        if (!healing)
        {
            Debug.Log("Heal");
            healing = true;
            lastTrigger = Time.time;
            theManager.currentMana -= theManager.HealMana;
            //HealCooldown.rectTransform.localScale = Empty;
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time >= lastTrigger + waitTime)
        {
            //HealCooldown.rectTransform.localScale = Full;
            healing = false;
        }
    }
}