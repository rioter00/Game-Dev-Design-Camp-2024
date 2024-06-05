using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblesToSpawn;
    //
    [SerializeField] List<Transform> spawnPoints;
    public int numRandomCollectibles = 3;
    
    
    private void Start()
    {
        platformSpawner.OnSpawn += Spawn;
    }

    private void OnDestroy()
    {
        platformSpawner.OnSpawn -= Spawn;
    }

    void Spawn()
    {
 
        var randomNum = Random.Range(0, 100);
        print("collectible spawn triggered. var: " + randomNum);
        if (randomNum > 100) return;
        List<Vector3> positions = RandomizeSpawnPoints();
        foreach (var position in positions)
        {
            print($"spawning collectibles at {positions}");
            var collectible = collectiblesToSpawn[Random.Range(0, collectiblesToSpawn.Length)];
            Instantiate(collectible, position, Quaternion.identity, null);
        }
    }

    List<Vector3> RandomizeSpawnPoints()
    {
        var randomNum = Random.Range(0, numRandomCollectibles);
        
        List<Transform> points = new List<Transform>();
        List<Vector3> positions = new List<Vector3>();
        // do
        // {
        //     var point = randomSpawnPoint();
        //     if (!positions.Contains(point))
        //     {
        //         // points.Add(point);
        //         positions.Add(point);
        //     }
        // } while (positions.Count < randomNum);
        for (int i = 0; i < randomNum; )
        {
            var point = randomSpawnPoint();
            if (!positions.Contains(point))
            {
                // points.Add(point);
                positions.Add(point);
                i++;
            }
        }
        return positions;
    }

    private Vector3 randomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
    }
}
