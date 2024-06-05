using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private CharactersConfig characters;

    [SerializeField] private Image characterImage;

    public int characterIndex;
    void Start()
    {
        characterIndex = characters.selectedIndex;
        displayCharacter();
    }

    public void nextCharacter()
    {
        characterIndex = characterIndex < characters.characters.Count-1 ? characterIndex+1 : characterIndex;
        print($"Character Index: {characterIndex}");
        print($"Character Index: {characters.selectedIndex}");
        displayCharacter();
    }

    public void previousCharacter()
    {
        characterIndex = characterIndex >= 1 ? characterIndex-1 : 0;
        displayCharacter();
    }

    public void displayCharacter()
    {
        characterImage.sprite = characters.characters[characterIndex].GetComponentInChildren<SpriteRenderer>().sprite;
        characterImage.preserveAspect = true;
        print($"Displaying: {characters.characters[characterIndex].GetComponentInChildren<SpriteRenderer>().sprite}");
    }

    public void loadGame()
    {
        updateCharacterIndex(characterIndex);
        SceneManager.LoadScene(1);
    }
    
    public void loadCredits()
    {
        updateCharacterIndex(characterIndex);
        SceneManager.LoadScene(2);
    }

    public void updateCharacterIndex(int index)
    {
        characters.selectedIndex = index;
        print($"Character Index: {characterIndex}");
    }

    public void setUsername(string username)
    {
        characters.username = username;
        print($"Setting username: {username}");
    }
}
