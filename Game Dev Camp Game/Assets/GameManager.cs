using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharactersConfig characters;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        print($"Character Index: {characters.selectedIndex} {characters.characters[characters.selectedIndex]}");
        Instantiate(characters.characters[characters.selectedIndex], spawnPoint.position, Quaternion.identity, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
