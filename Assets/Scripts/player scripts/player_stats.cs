using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_stats : MonoBehaviour
{
    [SerializeField] int max_health;
    [SerializeField] int current_health;
    [SerializeField] int max_mana;
    [SerializeField] int current_mana;

    public void update_health(int hd)
    {
        current_health += hd;
        if(current_health > max_health)
        {
            current_health = max_health;
        }
        else if(current_health <= 0)
        {
            gameover();
        }
    }

    public void gameover()
    {
        Debug.Log("Gameover");
    }
}
