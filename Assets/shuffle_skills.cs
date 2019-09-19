using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class shuffle_skills : MonoBehaviour
{
    //DirectoryInfo dir = new DirectoryInfo("/runes/");
    //FileInfo[] info;
    
    //public RawImage[] skills = new RawImage[5];
    public Sprite[] images;
    public bool islocked = false;
    string[] skill_icons;
    //public Sprite mysprite;
   // private RawImage imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        //runes = new char[]{'a','b','d','e','f','g','h','i','j' };
        skill_icons = new string[] { "skill1", "skill2", "skill3", "skill4", "skill5", "skill6", "skill7"};
        
        //images[0] = Resources.Load<Sprite>("rune_a");
        //gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("runes/rune_a");//images[Random.Range(0, images.Length)];
        
        //gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("runes/rune_a");
        
        //info = dir.GetFiles("*.");
        //skills = new RawImage[5]; //magic numbers are bad but it's a prototype
        //imageComponent = GetComponent<RawImage>();
        //imageComponent.texture = images[Random.Range(0, images.Length)].texture;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //GameObject.Find("Skill1")
                islocked = !islocked;
            
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
            if (GameObject.Find("Skill1").GetComponent<Image>().sprite.name == "rune_a")
                GameObject.Find("player").GetComponent<Light>().enabled = !GameObject.Find("player").GetComponent<Light>().enabled;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            if (GameObject.Find("Skill2").GetComponent<Image>().sprite.name == "rune_a")
                GameObject.Find("player").GetComponent<Light>().enabled = !GameObject.Find("player").GetComponent<Light>().enabled;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            if (GameObject.Find("Skill3").GetComponent<Image>().sprite.name == "rune_a")
                GameObject.Find("player").GetComponent<Light>().enabled = !GameObject.Find("player").GetComponent<Light>().enabled;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            if (GameObject.Find("Skill4").GetComponent<Image>().sprite.name == "rune_a")
                GameObject.Find("player").GetComponent<Light>().enabled = !GameObject.Find("player").GetComponent<Light>().enabled;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            if (GameObject.Find("Skill5").GetComponent<Image>().sprite.name == "rune_a")
                GameObject.Find("player").GetComponent<Light>().enabled = !GameObject.Find("player").GetComponent<Light>().enabled;
                */
        //foreach (FileInfo f in info)
        if (!islocked)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                //foreach (RawImage s in skills)
                //{
                string skillname = skill_icons[Random.Range(0, skill_icons.Length)];
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("runes/" + skillname);
                //}
            }
        }
    }
}
