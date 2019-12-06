using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable1 : MonoBehaviour
{
    public enum destroyType {normal, explode, burn};

    [Header("Health")]
    [SerializeField] float health = 1f;
    [Header("Destructable types")]
    [SerializeField] bool FireballsDestroy;
    [SerializeField] bool FirebreathsDestroy;
    [SerializeField] bool WallDestroy;
    [SerializeField] bool HealDestroy;
    [SerializeField] bool AOEDestroy;
    [SerializeField] bool TargetedAOEDestroy;
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
    [SerializeField] GameObject HealFlame = null;

    private AbilityManager AM;

    private void Start()
    {
        AM = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<AbilityManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        //Debug.Log("Collision");
        //Debug.Log(collision.gameObject.name + " asd");
        //Debug.Log(collision.gameObject.name);
        if ((collision.gameObject.tag == "Attack") && (canDestroy))
        {
            
            switch (collision.gameObject.name)
            {
                case "Fireball(Clone)":
                case "Fireball Variant(Clone)":
                    if (FireballsDestroy)
                    {
                        
                        health -= AM.FireballDMG;
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
                        health -= AM.FlamethrowDMG;
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
    }

    /// <summary>
    /// Trigger events of destroying the object.
    /// </summary>
    private void destruct()
    {
        switch (DestructionType)
        {
            case destroyType.normal:
                Destroy(gameObject);
                break;
            case destroyType.explode:
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
                if(HealFlame != null)
                {
                    HealFlame.SetActive(true);
                }
                MeshRenderer[] theRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
                
                foreach (MeshRenderer renderer in theRenderers)
                {
                    renderer.material = burnmat;
                }
                
                break;
        }
    }

    /// <summary>
    /// What happens when object is actually destroyed.
    /// </summary>
    private void OnDestroy()
    {
        switch (DestructionType)
        {
            case destroyType.normal:
                break;
            case destroyType.explode:
                break;
            case destroyType.burn:
                break;
        }
    }
    
    void Update()
    {
        if (timer > 0 && !canDestroy)
        {
            timer-= Time.deltaTime;
            //Debug.Log("Tick");
        }
        else if(timer <= 0 && !canDestroy)
        {
            canDestroy = true;
        }
    }
}
