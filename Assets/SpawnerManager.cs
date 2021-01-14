using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{

    private int spawnerCount;
    private float currentSpawnTimer = 0.0f;
    private float startSpawnTimer = 0.0f;

    private int spawnedCount = 0;
    public float spawnTimer = 5.0f;
    public float acceleratrionRatio = 0.9f;  

    private int currentSpawnLevel = 0;
    public int[] spawnLevelCount;
    public GameObject Ennemy;

    public UnityEvent eventOnEnnemyDeath;

    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        spawnerCount = transform.childCount;
        audioSources = new AudioSource[spawnerCount];
        for (int i = 0; i < spawnerCount; ++i)
        {
            audioSources[i] = transform.GetChild(i).gameObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTimer += Time.deltaTime;
        if(currentSpawnTimer >= spawnTimer)
        {
            for (int i = 0; i < currentSpawnLevel + 1; i++)
            {
                int childIndex = Random.Range(0, spawnerCount);
                Vector3 spawnLocation = transform.GetChild(childIndex).position;
                GameObject newEnnemy = Instantiate(Ennemy, spawnLocation, Quaternion.identity);
                Enemy component = newEnnemy.GetComponent<Enemy>();
                if (component)
                {
                    component.eventOnDeath.AddListener(OnEnnemyDeath);
                }
                if (audioSources[childIndex])
                {
                    audioSources[childIndex].Play();
                }
            }
            spawnedCount++;
            if (currentSpawnLevel < spawnLevelCount.Length && spawnLevelCount[currentSpawnLevel] < spawnedCount)
            {
                startSpawnTimer = 0.0f;
                spawnedCount = 0;
                currentSpawnLevel++;
            }
            else
            {
                startSpawnTimer = (spawnTimer - startSpawnTimer) * acceleratrionRatio;
            }
            currentSpawnTimer = startSpawnTimer;
        }
    }

    void OnEnnemyDeath()
    {
        eventOnEnnemyDeath.Invoke();
    }
}
