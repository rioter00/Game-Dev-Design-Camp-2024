using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class enemySpawner : MonoBehaviour
{
    //
    public bool BeginTimerAtStart = false;
    public GameObject LeftEnemy;
    public GameObject RightEnemy;
    public float SpawnInterval;
    public float SpawnIntervalIncrement = 0.02f;
    public float currentTime;

    //
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    //
    [Header("Play a sound when spawning? - Be sure to check the volume")]
    public AudioClip soundfile;
    AudioSource audioSource;
     

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
        }
    }

    void Spawn()
    {
        var point = RandomizeSpawnPoints();
        if (point == leftSpawnPoint)
        {
            Instantiate(LeftEnemy, point.position, Quaternion.identity, null);
        }
        else
        {
            Instantiate(RightEnemy, point.position, Quaternion.identity, null);
        }

    }

    Transform RandomizeSpawnPoints()
    {
        var ranVal = Random.Range(0, 2);
        print("Random enemy " + ranVal);
        if (ranVal == 1)
        {
            return leftSpawnPoint;
        }
        else
        {
            return rightSpawnPoint;
        }
    }
}

