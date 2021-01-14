using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    private GameObject playerObj = null;
    private bool isDead = false;
    private Stunable stunComponent = null;
    private NavMeshAgent agent = null;
    private ParticleSystem emiter = null;
    private bool isToBeDespawn = false;
    public float pushValue = 7.0f;

    public UnityEvent eventOnDeath;

    public AudioSource audioSourceDeath, audioSourceImpact; 

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        if(stunComponent == null)
        {
            stunComponent = GetComponent<Stunable>();
        }
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if(emiter == null)
        {
            emiter = GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stunComponent.IsStun())
        {
            return;
        }

        if (!isDead)
        {
            agent.enabled = true;
            if (Vector3.Distance(agent.destination, playerObj.transform.position) > 1.0f)
            {
                agent.destination = playerObj.transform.position;
            }
            
        }
    }

     void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            stunComponent.StartStun();
            agent.enabled = false;
            Vector3 impactDirection = (gameObject.transform.position - hit.gameObject.transform.position).normalized;
            impactDirection.y = 0.0f;
            GetComponent<Rigidbody>().AddForce(impactDirection * pushValue, ForceMode.Impulse);
            var emitParams = new ParticleSystem.EmitParams();
            emiter.Emit(emitParams, 10);
            audioSourceImpact.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isToBeDespawn && other.gameObject.tag == "Despawn")
        {
            eventOnDeath.Invoke();
            audioSourceDeath.Play();
            Destroy(gameObject, 2.0f);
            isToBeDespawn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            Vector3 vel = agent.velocity;
            agent.enabled = false;
            isDead = true;
            if (GetComponent<Rigidbody>().velocity.magnitude < vel.magnitude)
            {
                GetComponent<Rigidbody>().velocity = vel;
            }
        }
    }
}
