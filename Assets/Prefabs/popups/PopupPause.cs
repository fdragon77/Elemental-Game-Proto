using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPause : MonoBehaviour
{
    public void pause()
    {
        GameController.gamespeed = 0;
    }

    public void play()
    {
        GameController.gamespeed = 1;
    }
}
