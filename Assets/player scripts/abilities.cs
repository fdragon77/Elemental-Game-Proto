using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class abilities : MonoBehaviour
{
    [SerializeField] public List<Image> abDisplay;
    [SerializeField] public List<Sprite> abSprites;
    [SerializeField] public List<int> abOrder;
    [SerializeField] public List<bool> abIsSet;
    [SerializeField] int chance;
    [SerializeField] public List<KeyCode> abKeys;
    [SerializeField] public UnityEvent ability1;
    [SerializeField] public UnityEvent ability2;
    [SerializeField] public UnityEvent ability3;
    [SerializeField] public UnityEvent ability4;
    [SerializeField] public UnityEvent ability5;
    // Update is called once per frame
    void Update()
    {
        if(Random.RandomRange(0,chance) == 1)
        {
            int i;
            List<int> temp = new List<int>();
            while (abOrder.Count > 0)
            {
                i = Random.RandomRange(0, abOrder.Count);
                temp.Add(abOrder[i]);
                abOrder.RemoveAt(i);
            }
            abOrder = temp;
        }
        int ii = 0;
        foreach(Image Imag in abDisplay)
        {
            Imag.sprite = abSprites[abOrder[ii]];
            ii++;
        }
        for (int iii = 0; iii < abKeys.Count; iii++)
        {
            if (Input.GetKeyDown(abKeys[abOrder[iii]]))
            {
                Debug.Log("Key Pressed: " + abKeys[abOrder[iii]].ToString());
                switch (iii)
                {
                    case 0:
                        ability1.Invoke();
                        break;
                    case 1:
                        ability2.Invoke();
                        break;
                    case 2:
                        ability3.Invoke();
                        break;
                    case 3:
                        ability4.Invoke();
                        break;
                    case 4:
                        ability5.Invoke();
                        break;
                }
            }
        }
    }
}
