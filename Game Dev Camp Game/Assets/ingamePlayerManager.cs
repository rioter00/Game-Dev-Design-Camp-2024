using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using LootLocker.Requests;

public class ingamePlayerManager : MonoBehaviour
{
    public InGameScore leaderboard;
    private GameManager gm; 

    void Start()
    {
        // StartCoroutine(SetupRoutine());
        // gm = GetComponent<GameManager>();
        // SetPlayerName();
    }

    public void SetPlayerName()
    {
        // LootLockerSDKManager.SetPlayerName(gm.characters.username, (response) =>
        // {
        //     if (response.success)
        //     {
        //         Debug.Log("Successfully set player name: " + gm.characters.username);
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
