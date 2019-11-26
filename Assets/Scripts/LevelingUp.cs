using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.Events;

public class LevelingUp : MonoBehaviour
{
    public enum upgrade { a, b, c };
    public enum level { basic, a, b, c };

    level ballLevel = level.basic;
    level wallLevel = level.basic;
    level breathLevel = level.basic;
    level AoELevel = level.basic;
    level DashLevel = level.basic;

    [Header("Fire ball upgrades")]
    [SerializeField] float ballaDamage = 2;
    [SerializeField] float ballaSpeed = 60f;
    //[SerializeField] GameObject ballb;
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

    //For the main upgrade popup.
    UnityAction NullAction;
    UnityAction upgradeBall;
    UnityAction upgradeWall;
    UnityAction upgradeBreath;
    UnityAction upgradeAoE;
    UnityAction upgradeDash;

    //for fireball upgrade popup
    UnityAction FBA;
    UnityAction FBB;
    UnityAction FBC;

    //for firewall popup
    UnityAction FWA;
    UnityAction FWB;
    UnityAction FWC;

    //for firebreath popup
    UnityAction BA;
    UnityAction BB;
    UnityAction BC;

    //for AoE popup
    UnityAction AA;
    UnityAction AB;
    UnityAction AC;

    //for dash popup
    UnityAction DA;
    UnityAction DB;
    UnityAction DC;

    private void Start()
    {
        Abilitys = gameObject.GetComponent<AbilityManager>();
        ball = gameObject.GetComponent<Fireball>();
        wall = gameObject.GetComponent<firewall>();
        breath = gameObject.GetComponent<Flamethrower>();
        AoE = gameObject.GetComponent<KnockbackAbility>();

        //for main popup
        NullAction += nothing;
        upgradeBall += fireballPopup;
        upgradeWall += fireWallPopup;
        upgradeBreath += fireBreathPopup;
        upgradeAoE += AoEPopup;
        upgradeDash += DashPopup;

        //for fireball upgrade popup
        FBA += powerballup;
        FBB += rapidfireup;
        FBC += bombup;

        //for firewall popup
        FWA += powerwallup;
        FWB += tornadoup;
        FWC += trapup;

        //for firebreath popup
        BA = powerbreathup;
        BB = lavalancheup;
        BC = chainup;

        //for AoE popup
        AA += powerAoEup;
        AB += earthquakeup;
        AC += minionsup;

        //for dash popup
        DA += powerdashup;
        DB += chargeup;
        DC += eruptup;
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
                    //Debug.Log("upgraded fireball a");
                    break;
                case upgrade.b:
                    ball.Multishot = true;
                    //ball.projectile = ballb;
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

    public void popup()
    {
        UIPopup q_popup = UIPopup.GetPopup("levelup");

        if (q_popup is null)
        {
            Debug.Log("The popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsCallbacks(fireballPopup, fireWallPopup, fireBreathPopup, AoEPopup, DashPopup);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void fireballPopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("ThreeOptions");

        if (q_popup is null)
        {
            Debug.Log("The fireball popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsLabels("Powerball", "Multishot", "bomb");
        q_popup.Data.SetButtonsCallbacks(FBA, FBB, FBC);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void fireWallPopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("ThreeOptions");

        if (q_popup is null)
        {
            Debug.Log("The wall popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsLabels("Powerful Wall", "Tornado", "Meteor Trap");
        q_popup.Data.SetButtonsCallbacks(FWA, FWB, FWC);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void fireBreathPopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("ThreeOptions");

        if (q_popup is null)
        {
            Debug.Log("The breath popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsLabels("Power Breath", "Lavalanche", "Chain flame");
        q_popup.Data.SetButtonsCallbacks(BA, BB, BC);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void AoEPopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("ThreeOptions");

        if (q_popup is null)
        {
            Debug.Log("The AoE popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsLabels("Power throwback", "Earthquake", "Minions");
        q_popup.Data.SetButtonsCallbacks(AA, AB, AC);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    public void DashPopup()
    {
        UIPopup q_popup = UIPopup.GetPopup("ThreeOptions");

        if (q_popup is null)
        {
            Debug.Log("The Dash popup wasn't found!");
            return;
        }
        q_popup.Data.SetButtonsLabels("power dash", "Charge", "Erupt");
        q_popup.Data.SetButtonsCallbacks(DA, DB, DC);
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    //For unity events below here. 
    public void nothing()
    {

    }

    public void powerballup()
    {
        ballLevel = level.a;
        fireballupgrade(upgrade.a);
    }

    public void rapidfireup()
    {
        ballLevel = level.b;
        fireballupgrade(upgrade.b);
    }

    public void bombup()
    {
        ballLevel = level.c;
        fireballupgrade(upgrade.c);
    }

    public void powerbreathup()
    {
        breathLevel = level.a;
        BreathUpgrade(upgrade.a);
    }
    public void lavalancheup()
    {
        breathLevel = level.b;
        BreathUpgrade(upgrade.b);
    }
    public void chainup()
    {
        breathLevel = level.c;
        BreathUpgrade(upgrade.c);
    }

    public void powerAoEup()
    {
        AoELevel = level.a;
        AoEUpgrade(upgrade.a);
    }
    public void earthquakeup()
    {
        AoELevel = level.b;
        AoEUpgrade(upgrade.b);
    }
    public void minionsup()
    {
        AoELevel = level.c;
        AoEUpgrade(upgrade.c);
    }

    public void powerwallup()
    {
        wallLevel = level.a;
        fireWALLupgrade(upgrade.a);
    }
    public void tornadoup()
    {
        wallLevel = level.b;
        fireWALLupgrade(upgrade.b);
    }
    public void trapup()
    {
        wallLevel = level.c;
        fireWALLupgrade(upgrade.c);
    }
    public void powerdashup()
    {
        DashLevel = level.a;
        DashUpgrade(upgrade.a);
    }
    public void chargeup()
    {
        DashLevel = level.b;
        DashUpgrade(upgrade.b);
    }
    public void eruptup()
    {
        DashLevel = level.c;
        DashUpgrade(upgrade.c);
    }
}
