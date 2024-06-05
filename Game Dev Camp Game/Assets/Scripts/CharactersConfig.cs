using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObjects/Characters", order = 1)]
      
public class CharactersConfig : ScriptableObject
{
    public List<GameObject> characters;

    public int selectedIndex;

    public string username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
