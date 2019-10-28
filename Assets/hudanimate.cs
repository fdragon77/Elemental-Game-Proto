using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hudanimate : MonoBehaviour
{
    private RawImage barRawImage;
    // Start is called before the first frame update
    void Start()
    {
        barRawImage = transform.Find("mana bar fill").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= .3f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

    }
}
