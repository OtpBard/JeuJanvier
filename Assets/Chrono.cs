using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour
{
    public float contdownTimerSeconds = 120.0f;
    public Text chronoText;
    private float timer;

    void Start()
    {
        timer = contdownTimerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timer);
        chronoText.text = timeSpan.ToString(@"mm\:ss");
        EndGameStats.remainingTime = timer;
        if (timer < 0.0f)
        {
            EndGameStats.remainingTime = 0.0f;
            GetComponent<SceneChange>().ChangeScene();
        }
    }
}
