using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;

    void Start()
    {
        if (Clock.instance != null)
        {
            textClock.text = Clock.instance.GetCurrentTimeText();
        }
    }
}
