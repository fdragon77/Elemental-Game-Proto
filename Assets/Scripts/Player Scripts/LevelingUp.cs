using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.Events;

public class LevelingUp : MonoBehaviour
{
    //Used for basic types of upgrades.
    public enum upgrade { a, b, c };
    //Used for what level/upgrades are used. 
    public enum level { basic, a, b, c };

    //Set the default level for all abilities.
    level ballLevel = level.basic;
    level wallLevel = level.basic;
    level breathLevel = level.basic;
    level AoELevel = level.basic;
    level DashLevel = level.basic;

    //All the stuff used for upgrading abilities for editor.
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

    //References to the players abilities scripts. 
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
        //Get references to player abilites. 
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
                //Basic upgrade to fireball.
                case upgrade.a:
                    ball.fireballSpeed = ballaSpeed;
                    Abilitys.FireballDMG = ballaDamage;
                    ballLevel = level.a;
                    break;
                //Upgrade to multishot.
                case upgrade.b:
                    ball.Multishot = true;
                    Debug.Log("upgraded fireball multishot");
                    ballLevel = level.b;
                    break;
                //Upgrade to explosion shot. 
                case upgrade.c:
                    ball.projectile = ballc;
                    ballLevel = level.c;
                    break;
            }
        }
        else
        {
            //popup();
        }
    }

    public void fireWALLupgrade(upgrade u)
    {
        if (wallLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    //wall.projectile = walla;
                    wallLevel = level.a;
                    break;
                case upgrade.b:
                    //wall.projectile = wallb;
                    wallLevel = level.b;
                    break;
                case upgrade.c:
                    //wall.projectile = wallc;
                    wallLevel = level.c;
                    break;
            }
        }
        else
        {
            //popup();
        }
    }

    public void BreathUpgrade(upgrade u)
    {
        if (breathLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    breathLevel = level.a;
                    break;
                case upgrade.b:
                    breathLevel = level.b;
                    break;
                case upgrade.c:
                    breathLevel = level.c;
                    break;
            }
        }
        else
        {
            //popup();
        }
    }

    public void AoEUpgrade(upgrade u)
    {
        if (AoELevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    AoELevel = level.a;
                    break;
                case upgrade.b:
                    AoELevel = level.b;
                    break;
                case upgrade.c:
                    AoELevel = level.c;
                    break;
            }
        }
        else
        {
            //popup();
        }
    }

    public void DashUpgrade(upgrade u)
    {
        if (DashLevel == level.basic)
        {
            switch (u)
            {
                case upgrade.a:
                    DashLevel = level.a;
                    break;
                case upgrade.b:
                    DashLevel = level.b;
                    break;
                case upgrade.c:
                    DashLevel = level.c;
                    break;
            }
        }
        else
        {
            //popup();
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
        q_popup.Data.Buttons[0].SelectButton();
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = true;
        q_popup.HideOnBackButton = true;
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
        q_popup.Data.SetLabelsTexts("Fireball upgrades!", "");
        q_popup.Data.SetButtonsLabels("Powerball", "Multishot", "bomb");
        q_popup.Data.SetButtonsCallbacks(FBA, FBB, FBC);
        q_popup.Data.Buttons[0].SelectButton();
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
        q_popup.Data.SetLabelsTexts("Firewall upgrades!", "These have not been implimented yet. Press back to go back to choices.");
        q_popup.Data.SetButtonsLabels("Powerful Wall", "Tornado", "Meteor Trap");
        q_popup.Data.SetButtonsCallbacks(FWA, FWB, FWC);
        q_popup.Data.Buttons[0].SelectButton();
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
        q_popup.Data.SetLabelsTexts("Fire breath upgrades!", "These have not been implimented yet. Press back to go back to choices.");
        q_popup.Data.SetButtonsLabels("Power Breath", "Lavalanche", "Chain flame");
        q_popup.Data.SetButtonsCallbacks(BA, BB, BC);
        q_popup.Data.Buttons[0].SelectButton();
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
        q_popup.Data.SetLabelsTexts("AOE upgrades!", "These have not been implimented yet. Press back to go back to choices.");
        q_popup.Data.SetButtonsLabels("Power throwback", "Earthquake", "Minions");
        q_popup.Data.SetButtonsCallbacks(AA, AB, AC);
        q_popup.Data.Buttons[0].SelectButton();
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
        q_popup.Data.SetLabelsTexts("Dash upgrades!", "These have not been implimented yet. Press back to go back to choices.");
        q_popup.Data.SetButtonsLabels("power dash", "Charge", "Erupt");
        q_popup.Data.SetButtonsCallbacks(DA, DB, DC);
        q_popup.Data.Buttons[0].SelectButton();
        q_popup.HideOnAnyButton = true;
        q_popup.HideOnClickOverlay = false;
        q_popup.HideOnBackButton = false;
        q_popup.HideOnClickContainer = false;

        q_popup.Show();
    }

    //For unity events below here. 
    public void nothing()
    {
        //placeholder for buttons that don't trigger any events. 
    }

    public void powerballup()
    {
        Debug.Log("upgrade powerball up");
        fireballupgrade(upgrade.a);
    }

    public void rapidfireup()
    {
        Debug.Log("upgrade multishot");
        fireballupgrade(upgrade.b);
    }

    public void bombup()
    {
        fireballupgrade(upgrade.c);
    }

    public void powerbreathup()
    {
        BreathUpgrade(upgrade.a);
    }
    public void lavalancheup()
    {
        BreathUpgrade(upgrade.b);
    }
    public void chainup()
    {
        BreathUpgrade(upgrade.c);
    }

    public void powerAoEup()
    {
        AoEUpgrade(upgrade.a);
    }
    public void earthquakeup()
    {
        AoEUpgrade(upgrade.b);
    }
    public void minionsup()
    {
        AoEUpgrade(upgrade.c);
    }

    public void powerwallup()
    {
        fireWALLupgrade(upgrade.a);
    }
    public void tornadoup()
    {
        fireWALLupgrade(upgrade.b);
    }
    public void trapup()
    {
        fireWALLupgrade(upgrade.c);
    }
    public void powerdashup()
    {
        DashUpgrade(upgrade.a);
    }
    public void chargeup()
    {
        DashUpgrade(upgrade.b);
    }
    public void eruptup()
    {
        DashUpgrade(upgrade.c);
    }
}
