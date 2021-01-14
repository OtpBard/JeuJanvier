using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScore : MonoBehaviour
{
    public Text scoreText;
    void Start()
    {
         scoreText.text = EndGameStats.score.ToString();
    }
}
