using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class platformSpawner : MonoBehaviour
{
    //
    public bool BeginTimerAtStart = false;
    public GameObject platformToSpawn;
    public GameObject fallingPlatformPrefab;
    public float SpawnInterval;
    public float SpawnIntervalIncrement = 0.02f;
    public float currentTime;

    //
    [SerializeField] List<Transform> spawnPoints;
    public int numRandomPlatforms = 3;
    
    //
    [Header("Play a sound when spawning? - Be sure to check the volume")]
    public AudioClip soundfile;
    AudioSource audioSource;
    
    //
    public delegate void SpawnEvent();

    public static SpawnEvent OnSpawn;
    

    private void Start()
    {
        if (BeginTimerAtStart) currentTime = SpawnInterval;
        audioSource = GetComponent<AudioSource>();
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (soundfile != null) audioSource.clip = soundfile;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        } else
        {
            Spawn();
            SpawnInterval -= SpawnIntervalIncrement;
            currentTime = SpawnInterval;
            
            Invoke(nameof(TriggerCollectibles), 1.2f);
        }
    }

    void OnDestroy()
    {
        CancelInvoke();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void TriggerCollectibles()
    {
        OnSpawn?.Invoke();
    }

    void Spawn()
    {
        var points = RandomizeSpawnPoints();
        foreach (var point in points)
        {
            // Instantiate(platformToSpawn, point.position, Quaternion.identity, null);
            var ranChance = Random.Range(0, 100);
            if (ranChance > 80)
            {
                Instantiate(fallingPlatformPrefab, point.position, Quaternion.identity, null);
            }
            else
            {
                Instantiate(platformToSpawn, point.position, Quaternion.identity, null);
            }
        }
    }

    List<Transform> RandomizeSpawnPoints()
    {
        var ranPlats = Random.Range(2, numRandomPlatforms);
        List<Transform> points = new List<Transform>();
        do
        {
            var point = randomSpawnPoint();
            if (!points.Contains(point))
            {
                points.Add(point);
            }
        } while (points.Count < ranPlats);
        return points;
    }

    private Transform randomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
