using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AoeBlast : MonoBehaviour
{
    public GameObject aoeRing;
    [SerializeField] GameObject AoeObject;
    float timer = 0;
    bool active = false;
    [SerializeField] float cooldown;
    GameObject Blast;

    public RawImage AoeCooldown;

    Vector3 Empty = new Vector3(0, 1, 1);
    Vector3 Full = new Vector3(1, 1, 1);
    AbilityManager theManager;
    float ratio;

    Vector4 color;
    Vector4 fadeColor = new Vector4(60, 60, 60, 0);
    // Start is called before the first frame update
    void Start()
    {
        AoeCooldown = GameObject.Find("AoeFill").GetComponent<RawImage>();
        color = AoeCooldown.color;
        theManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        timer = cooldown;
    }
    public void Fire()
    {
        if (!active)
        {
            Debug.Log("AoeBlast");
            timer = 0;
            Blast = Instantiate(AoeObject, transform.position, AoeObject.transform.rotation) as GameObject;
            //aoeRing.SetActive(true);
            active = true;
            //theManager.currentMana -= theManager.AoeMana;
            //AoeCooldown.rectTransform.localScale = Empty;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timer <= cooldown)
        {
            timer += Time.deltaTime;
        }
        
        if (timer >= cooldown && active)
        {
            //aoeRing.SetActive(false);
            active = false;
            //AoeCooldown.rectTransform.localScale = Full;
        }
        ratio= timer / cooldown;
        AoeCooldown.rectTransform.localScale = new Vector3(ratio, 1, 1);
        if (theManager.currentMana < theManager.AoeMana)
        {
            AoeCooldown.color = fadeColor;
        }
        else
        {
            AoeCooldown.color = color;
        }
    }
}
