using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable1 : MonoBehaviour
{
    public enum destroyType {normal, explode, burn};

    [Header("Destructable types")]
    [SerializeField] bool FireballsDestroy;
    [SerializeField] bool FirebreathsDestroy;
    [SerializeField] bool BeamDestroy;
    [SerializeField] bool HealDestroy;
    [SerializeField] bool AOEDestroy;
    [SerializeField] bool TargetedAOEDestroy;

    [Header("How Does this become destroyed?")]
    [SerializeField] destroyType DestructionType = destroyType.normal;

    [SerializeField] Material burnmat;
    [SerializeField] GameObject explodeObj;

    [Header("Misc")]
    [SerializeField] GameObject HealFlame = null;

    private void OnTriggerEnter(Collider collision)
    {
        
        Debug.Log("Collision");
        Debug.Log(collision.gameObject.name + " asd");
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Attack")
        {
            
            switch (collision.gameObject.name)
            {
                case "Fireball(Clone)":
                    if (FireballsDestroy)
                    {
                        destruct();
                    }
                    break;
                case "fireCone":
                    if (FirebreathsDestroy)
                    {
                        Debug.Log("Breath Destroy");
                        destruct();
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
                Instantiate(explodeObj, pos, new Quaternion());
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
}
