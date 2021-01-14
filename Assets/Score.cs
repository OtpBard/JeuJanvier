using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
   private int currentScore = 0;
   public Text scoreText;
    void Update()
    {
        scoreText.text = currentScore.ToString();

    }

    public void RegisterKill()
    {
        currentScore++;
        EndGameStats.score = currentScore;
    }
}
