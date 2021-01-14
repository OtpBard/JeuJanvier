using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunable : MonoBehaviour
{
    private bool stun = false;
    public float stunDuration = 0.5f;
    private float currentStunTimer = 0.0f;

    public bool IsStun()
    {
        return stun;
    }

    public void StartStun()
    {
        stun = true;
    }

    public void EndStun()
    {
        stun = false;
        currentStunTimer = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stun)
        {
            currentStunTimer += Time.deltaTime;
            if (currentStunTimer >= stunDuration) 
            {
                EndStun();
            }
        }
    }
}
