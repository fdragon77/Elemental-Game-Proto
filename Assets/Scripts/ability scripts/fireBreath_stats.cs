using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBreath_stats : MonoBehaviour
{
    [SerializeField] int generationsLeft;
    [SerializeField] float maxDistance;
    [SerializeField] public float rotationDegree;
    [SerializeField] float speed;
    [SerializeField] GameObject spawn;
    [HideInInspector] Vector3 startpos;
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if(Vector3.Distance(startpos, transform.position) > maxDistance)
        {
            spawnNextGen();
        }
    }

    public void spawnNextGen()
    {
        if (generationsLeft > 0)
        {
            GameObject inst1 = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
            GameObject inst2 = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
            GameObject inst3 = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);
            GameObject inst4 = Instantiate(spawn, gameObject.transform.position, gameObject.transform.rotation);

            inst1.transform.Rotate(0, rotationDegree, 0);
            inst2.transform.Rotate(0, rotationDegree * -1, 0);
            inst3.transform.Rotate(0, 0, rotationDegree);
            inst4.transform.Rotate(0, 0, rotationDegree * -1);

            inst1.GetComponent<fireBreath_stats>().generationsLeft = generationsLeft - 1;
            inst2.GetComponent<fireBreath_stats>().generationsLeft = generationsLeft - 1;
            inst3.GetComponent<fireBreath_stats>().generationsLeft = generationsLeft - 1;
            inst4.GetComponent<fireBreath_stats>().generationsLeft = generationsLeft - 1;
        }

        Destroy(gameObject);
    }
}
