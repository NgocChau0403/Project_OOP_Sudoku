using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{   

    public Text textClock;

    void Start()
    {
            textClock.text = Clock.instance.GetCurrentTimeText().text;   
    }
}



