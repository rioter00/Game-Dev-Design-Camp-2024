//used to log in players to leaderboard
//reference: https://youtu.be/u8llsk7FoYg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//***
// using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public InGameScore leaderboard;
    public TMP_InputField playerNameInputField;

    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        // LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        // {
        //     if (response.success)
        //     {
        //         Debug.Log("Successfully set player name");
        //     }
        //     else
        //     {
        //         Debug.Log("Could not set player name" + response.Error);
        //     }
        // });
    }

    //IEnumerator used instead of a function since the sever is being called (which is not instant)
    IEnumerator LoginRoutine() //Must be performed before the leaderboard is used
    {
        bool done = false;
        // LootLockerSDKManager.StartGuestSession((response) =>
        // {
        //     if (response.success)
        //     {
        //         Debug.Log("Player was logged in");
        //         PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
        //         done = true;
        //     }
        //     else
        //     {
        //         Debug.Log("Could not start session");
        //         done = true;
        //     }
        // });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    void Update()
    {
        
    }
}
