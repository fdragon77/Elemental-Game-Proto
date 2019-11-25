using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class armor : MonoBehaviour
{

    private int health = 100;
    public Text healthText;
    public Sprite armor1;
    Sprite[] armorSprites;
    private void Awake()
    {
        LoadAllSprites();
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = (armor1);
        

    }

    void Update()
    {
        healthText.text = "Health : " + health;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health--;

            armor1 = armorSprites[100 - health];

        }
        this.gameObject.GetComponent<Image>().sprite = (armor1);
       
    }

   
    void LoadAllSprites()
    {

        armorSprites = Resources.LoadAll<Sprite>("armor_stuff");
       
        

        armor1 = armorSprites[100 - health];
        
        foreach (Sprite s in armorSprites)
        {
            Debug.Log(s.name);
            
        }
    


    }
}