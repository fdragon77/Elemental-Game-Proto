using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destructable : MonoBehaviour
{
    public enum destroyType {normal, explode, burn, trigger};

    [Header("Health")]
    [SerializeField] float health = 1f;
    [Header("Destructable types")]
    [SerializeField] bool FireballsDestroy;
    [SerializeField] bool FirebreathsDestroy;
    [SerializeField] bool WallDestroy;
    [SerializeField] bool HealDestroy;
    [SerializeField] bool AOEDestroy;
    [SerializeField] bool TargetedAOEDestroy;
    [SerializeField] bool Touch;
    [SerializeField] int explosiveForce;
    [SerializeField] float GracePeriod;
    bool canDestroy = true;
    float timer = 0;
    GameObject Tourge;
    [Header("How Does this become destroyed?")]
    [SerializeField] destroyType DestructionType = destroyType.normal;

    [SerializeField] Material burnmat;
    [SerializeField] GameObject explodeObj;

    [Header("Misc")]
    [SerializeField] private bool CreateHeal = true;
    [SerializeField] GameObject HealFlame = null;

    private AbilityManager AM;
    private GameObject DamageDisplay;

    [Header("Touch damage timer")]
    [SerializeField] float TouchTime = 05f;
    [SerializeField] float ThrowerTime = 0.1f;
    private bool Touching = false;
    private bool Throwing = false;
    private float TouchTimer;
    private float ThrowTimer;

    private void Start()
    {
        AM = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<AbilityManager>();
        DamageDisplay = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().DamageCounter;
        if (HealFlame == null)
        {
            HealFlame = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().HealFlame;
        }
        //timers for extended damage.
        TouchTimer = Time.time;
        ThrowTimer = Time.time;
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        //Debug.Log("Collision");
        //Debug.Log(collision.gameObject.name + " asd");
        //Debug.Log(collision.gameObject.name);
        if ((collision.gameObject.tag == "Attack") && (canDestroy))
        {
            if (CreateHeal)
            {
                Instantiate(HealFlame, transform.position, transform.rotation);
            }
            switch (collision.gameObject.name)
            {
                case "Fireball(Clone)":
                case "Fireball Variant(Clone)":
                    if (FireballsDestroy)
                    {
                        
                        health -= AM.FireballDMG;
                        displayDamage((AM.FireballDMG * 10).ToString());
                        Debug.Log("Damage" + AM.FireballDMG.ToString());
                        Debug.Log(health);
                        if (health <= 0)
                        {
                            destruct();
                        }
                        else
                        {
                            canDestroy = false;
                            Debug.Log("GraceBall");
                            timer = GracePeriod;
                        }
                    }
                    break;
                case "fireCone":
                    if (FirebreathsDestroy)
                    {
                        Throwing = true;
                        ThrowTimer = Time.time;
                        health -= AM.FlamethrowDMG;
                        displayDamage((AM.FlamethrowDMG * 10).ToString());
                        if (health <= 0)
                        {
                            destruct();
                        }
                        else
                        {
                            canDestroy = false;
                            timer = GracePeriod;
                        }
                    }
                    break;
                case "fire wall(Clone)":
                    if (FirebreathsDestroy)
                    {
                        health -= AM.FirewallDMG;
                        displayDamage((AM.FirewallDMG * 10).ToString());
                        if (health <= 0)
                        {
                            destruct();
                        }
                        else
                        {
                            canDestroy = false;
                            timer = GracePeriod;
                        }
                    }
                    break;
                case "AoeDam":
                case "AoeKnock":
                    if (AOEDestroy)
                    {
                        health -= AM.AoeDMG;
                        displayDamage((AM.AoeDMG * 10).ToString());
                        if (health <= 0)
                        {
                            destruct();
                        }
                        else
                        {
                            canDestroy = false;
                            timer = GracePeriod;
                        }
                    }
                    break;

                    //FIXME Fill in with prefabs as we work on it.
            }
        }
        else if((collision.gameObject.tag == "Player") && (canDestroy) && Touch)
        {
            Touching = true;
            TouchTimer = Time.time;
            if (CreateHeal)
            {
                Instantiate(HealFlame, transform.position, transform.rotation);
            }
            health -= 1;
            displayDamage("5");
            if (health <= 0)
            {
                destruct();
            }
            else
            {
                canDestroy = false;
                timer = GracePeriod;
            }
        }
        else if (collision.gameObject.tag == "burnTile")
        {
            Touching = true;
            TouchTimer = Time.time;
            if (CreateHeal)
           {
                Instantiate(HealFlame, transform.position, transform.rotation);
            }
            health -= 1;
            displayDamage("5");
            if (health <= 0)
            {
                destruct();
            }
            else
            {
                canDestroy = false;
                timer = GracePeriod;
            }
        }
    }

    /// <summary>
    /// Trigger events of destroying the object.
    /// </summary>
    private void destruct()
    {
        switch (DestructionType)
        {
            case destroyType.normal:
                enemyCheck();
                Destroy(gameObject);
                break;
            case destroyType.explode:
                enemyCheck();
                Vector3 pos = transform.position;
                Tourge= Instantiate(explodeObj, pos, transform.rotation);
                Rigidbody[] theBodies = Tourge.GetComponentsInChildren<Rigidbody>();
                if (theBodies.Length > 0)
                {
                    foreach (Rigidbody body in theBodies)
                    {
                        body.AddExplosionForce(explosiveForce, transform.position, 1);
                    }
                }
                

                Destroy(gameObject);
                break;
            case destroyType.burn:
                enemyCheck();
                if (HealFlame != null)
                {
                    HealFlame.SetActive(true);
                }
                MeshRenderer[] theRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
                
                foreach (MeshRenderer renderer in theRenderers)
                {
                    renderer.material = burnmat;
                }
                
                break;
            case destroyType.trigger:
                puzzleTorch T;
                if (gameObject.TryGetComponent<puzzleTorch>(out T))
                {
                    T.burn();
                }
                health = 1;
                break;
        }
    }

    private void enemyCheck()
    {
        if(gameObject.GetComponent<EnemyInSight>() != null)
        {
            gameObject.GetComponent<EnemyInSight>().RemoveThyself();
        }
    }

    /// <summary>
    /// What happens when object is actually destroyed.
    /// </summary>
    private void OnDestroy()
    {
        
    }
    
    void Update()
    {
        if (GameController.gamespeed > 0)
        {
            if (timer > 0 && !canDestroy)
            {
                timer -= Time.deltaTime;
                //Debug.Log("Tick");
            }
            else if (timer <= 0 && !canDestroy)
            {
                canDestroy = true;
            }

            if (Touching && TouchTimer + TouchTime <= Time.time)
            {
                TouchTimer = Time.time;
                if (CreateHeal)
                {
                    Instantiate(HealFlame, transform.position, transform.rotation);
                }
                health -= 1;
                displayDamage("5");
                if (health <= 0)
                {
                    destruct();
                }
                else
                {
                    canDestroy = false;
                    timer = GracePeriod;
                }
            }
            if (Throwing && ThrowTimer + ThrowerTime <= Time.time)
            {
                ThrowTimer = Time.time;
                health -= AM.FlamethrowDMG;
                displayDamage((AM.FlamethrowDMG * 10).ToString());
                if (health <= 0)
                {
                    destruct();
                }
                else
                {
                    canDestroy = false;
                    timer = GracePeriod;
                }
            }
        }
    }

    void displayDamage(string damage)
    {
        GameObject disp = Instantiate(DamageDisplay, transform.position + new Vector3(0, 10, 0), transform.rotation);
        disp.GetComponent<movingEnviroment>().Goal = disp.transform.up * 100;
        disp.GetComponent<TextMeshPro>().text = damage;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().addPoints(int.Parse(damage));
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "fireCone")
        {
            Throwing = false;
        }
        if (other.gameObject.tag == "Player")
        {
            Touching = false;
        }
    }
}
