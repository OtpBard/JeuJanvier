using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{

    public float moveMaxSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeedRatio = 0.5f;
    public float jumpHeight = 10.0f;
    private bool isInAir = false;

    
    public float pushValue = 7.0f;

    public Stunable StunComponent = null;
    // Start is called before the first frame update
    void Start()
    {
        if(StunComponent == null)
        {
            StunComponent = GetComponent<Stunable>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (StunComponent && StunComponent.IsStun())
        {
            return;
        }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

        if (isInAir)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalInput * moveMaxSpeed * jumpSpeedRatio, ForceMode.Force);
            GetComponent<Rigidbody>().AddForce(Vector3.forward * verticalInput * moveMaxSpeed * jumpSpeedRatio, ForceMode.Force);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalInput * moveMaxSpeed, ForceMode.Force);
            GetComponent<Rigidbody>().AddForce(Vector3.forward * verticalInput * moveMaxSpeed, ForceMode.Force);
        }
        

        Vector3 targetDirection = (Vector3.right * horizontalInput + Vector3.forward * verticalInput).normalized;
        if(targetDirection.magnitude > 0.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation , Time.deltaTime * rotationSpeed);
        }
       
        if (Input.GetKey("space") && !isInAir)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isInAir = true;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.tag == "Floor")
        {
            isInAir = false;
        }

        if(hit.gameObject.tag == "JumpStop")
        {
            StunComponent.stunDuration = 10.0f;
            StunComponent.StartStun();
        }

        if (hit.gameObject.tag == "Ennemy")
        {
            StunComponent.StartStun();
            Vector3 dir = (gameObject.transform.position - hit.gameObject.transform.position);
            dir.y = 0.0f;
            GetComponent<Rigidbody>().AddForce(dir.normalized * pushValue, ForceMode.Impulse);
        }
    }

     void OnCollisionExit(Collision hit)
    {
        if(hit.gameObject.tag == "Floor")
        {
            isInAir = true;
        }
    }

    IEnumerator GoGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<SceneChange>().ChangeScene();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Despawn")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(GoGameOverScreen());
        }
    }
}
