using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingUp : MonoBehaviour
{
    public enum upgrade {a, b, c};
    public enum level {basic, a, b, c};

    level ballLevel = level.basic;
    level wallLevel = level.basic;
    level breathLevel = level.basic;
    level AoELevel = level.basic;
    level DashLevel = level.basic;

    [Header("Fire ball upgrades")]
    [SerializeField] float ballaDamage = 2;
    [SerializeField] float ballaSpeed = 60f;
    [SerializeField] GameObject ballb;
    [SerializeField] GameObject ballc;
    [Header("Fire Breath upgrades")]
    [SerializeField] GameObject breatha;
    [SerializeField] GameObject breathb;
    [SerializeField] GameObject breathc;
    [Header("Fire Wall upgrades")]
    [SerializeField] GameObject walla;
    [SerializeField] GameObject wallb;
    [SerializeField] GameObject wallc;
    [Header("AoE upgrades")]
    [SerializeField] GameObject AoEa;
    [SerializeField] GameObject AoEb;
    [SerializeField] GameObject AoEc;

    private AbilityManager Abilitys;
    private Fireball ball;
    private Flamethrower breath;
    private firewall wall;
    private KnockbackAbility AoE;

    private void Start()
    {
        Abilitys = gameObject.GetComponent<AbilityManager>();
        ball = gameObject.GetComponent<Fireball>();
        wall = gameObject.GetComponent<firewall>();
        breath = gameObject.GetComponent<Flamethrower>();
        AoE = gameObject.GetComponent<KnockbackAbility>();
    }

    public void fireballupgrade(upgrade u)
    {
        if (ballLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    ball.fireballSpeed = ballaSpeed;
                    Abilitys.FireballDMG = ballaDamage;
                    Debug.Log("upgraded fireball a");
                    break;
                case upgrade.b:
                    ball.projectile = ballb;
                    break;
                case upgrade.c:
                    ball.projectile = ballc;
                    break;
            }
        }
    }

    public void fireWALLupgrade(upgrade u)
    {
        if (wallLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    wall.projectile = walla;
                    break;
                case upgrade.b:
                    wall.projectile = wallb;
                    break;
                case upgrade.c:
                    wall.projectile = wallc;
                    break;
            }
        }
    }

    public void BreathUpgrade(upgrade u)
    {
        if (breathLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    break;
                case upgrade.b:
                    break;
                case upgrade.c:
                    break;
            }
        }
    }

    public void AoEUpgrade(upgrade u)
    {
        if (AoELevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    break;
                case upgrade.b:
                    break;
                case upgrade.c:
                    break;
            }
        }
    }

    public void DashUpgrade(upgrade u)
    {
        if (DashLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    break;
                case upgrade.b:
                    break;
                case upgrade.c:
                    break;
            }
        }
    }

}
