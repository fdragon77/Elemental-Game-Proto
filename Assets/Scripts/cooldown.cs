using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    private RawImage barRawImage;
    // Start is called before the first frame update
    void Start()
    {
        barRawImage = transform.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= .1f * Time.deltaTime;
        barRawImage.uvRect = uvRect;
    }
}
