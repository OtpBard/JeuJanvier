using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameChrono : MonoBehaviour
{
    public Text chronoText;
    void Start()
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(EndGameStats.remainingTime);
        chronoText.text = timeSpan.ToString(@"mm\:ss");
    }
}
