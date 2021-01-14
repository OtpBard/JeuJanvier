using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDeath : MonoBehaviour
{
    public GameObject emiterObject = null;
    private ParticleSystem emiter = null;
    public int particleCount = 10;

    // Start is called before the first frame update
    void Start()
    {
         if(emiter == null)
        {
            emiter = emiterObject.GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            var emitParams = new ParticleSystem.EmitParams();
            emiterObject.transform.position = new Vector3(other.gameObject.transform.position.x, emiterObject.transform.position.y,other.gameObject.transform.position.z);
            emiter.Emit(emitParams, particleCount);
        }

        if (other.gameObject.tag == "Player")
        {
            var emitParams = new ParticleSystem.EmitParams();
            emiterObject.transform.position = new Vector3(other.gameObject.transform.position.x, emiterObject.transform.position.y,other.gameObject.transform.position.z);
            emiter.Emit(emitParams, particleCount*2);
        }
    }
}
