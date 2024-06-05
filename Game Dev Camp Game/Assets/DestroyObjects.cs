using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [Header("Destroy which objects of tag")]
    public bool destroyAllObjects;
    public bool destroyPlatforms;
    public bool destroyCollectibles;
    public bool destroyEnemies;
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        DestroyIt(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyIt(other.gameObject);
    }

    void DestroyIt(GameObject go)
    {
        if (go.CompareTag("Player"))
        {
            return;
        }
        
        print("Destroy it? " + go.tag);
        if (destroyAllObjects)
        {
            Destroy(go);
            return;
        }
        if (destroyPlatforms && go.CompareTag("Platform"))
        {
            Destroy(go);
            return;
        }
        if (destroyCollectibles && go.CompareTag("Collectible"))
        {
            Destroy(go);
            return;
        }
        if (destroyEnemies && go.CompareTag("Enemy"))
        {
            Destroy(go);
            return;
        }
    }
    
}
