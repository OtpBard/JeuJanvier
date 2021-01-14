using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    public float jumpTimeInterval = 2.0f;
    public float maxJumpTimeOffset = 0.0f;
    public float minJumpHeight = 5.0f;
    public float maxJumpHeight = 10.0f;
    private float jumpHeight;
    private float currentJumpTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentJumpTime = Random.Range(0.0f, maxJumpTimeOffset);
        jumpHeight = Random.Range(minJumpHeight, maxJumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        currentJumpTime += Time.deltaTime;

        if (currentJumpTime >= jumpTimeInterval)
        {
            currentJumpTime = 0.0f;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
