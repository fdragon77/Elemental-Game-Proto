using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class armor : MonoBehaviour
{

    private int health = 100;
    //public Text healthText;
    public Sprite armor1;
    Sprite[] armorSprites;
    private CharacterController watchHealth;
    private void Awake()
    {
        
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = (armor1);
        watchHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        LoadAllSprites();

    }

    void Update()
    {
        //healthText.text = "Health : " + health;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health--;

            Debug.Log(watchHealth.health);

        }
        if (watchHealth.health >0)
        {
            armor1 = armorSprites[100 - watchHealth.health];
        }
        else
        {
            armor1 = armorSprites[99];
        }
        
        this.gameObject.GetComponent<Image>().sprite = (armor1);
       
    }

   
    void LoadAllSprites()
    {

        armorSprites = Resources.LoadAll<Sprite>("armor_stuff");
       
        

        armor1 = armorSprites[100 - watchHealth.health];
        
        foreach (Sprite s in armorSprites)
        {
            Debug.Log(s.name);
            
        }
    


    }
}