//reference: https://youtu.be/u8llsk7FoYg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//***
using LootLocker.Requests;
using TMPro;

public class menuScore : MonoBehaviour
{
    
    int leaderboardID = 8390;
    public TMP_Text[] scores;
    public int numScores; 
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    
    public TMP_InputField playerNameInputField;

    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name: " + playerNameInputField.text);
                
            }
            else
            {
                Debug.Log("Could not set player name" + response.Error);
            }
        });
        
        GetComponent<CharacterSelect>().setUsername(playerNameInputField.text);
    }

    //IEnumerator used instead of a function since the sever is being called (which is not instant)
    IEnumerator LoginRoutine() //Must be performed before the leaderboard is used
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return FetchTopHighscoresRoutine();
    }

    //used to grab multiple highscores
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, numScores, 0, (response) => //the second int (1) indicates how many top scoring places to be displayed while the third int (0) displays the bottom scores
        {
            if (response.success)
            {


                LootLockerLeaderboardMember[] members = response.items;


                for (int i = 0; i < members.Length; i++)
                {
                    string tempPlayerNames = "Name";
                    string tempPlayerScores = "Score\n";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames = members[i].player.name;
                        print($"Members: {members[i].player.name}");
                    }
                    else
                    {
                        tempPlayerNames = members[i].player.id.ToString();
                    }
                    tempPlayerScores = members[i].score.ToString();
                    // tempPlayerNames = ;
                    scores[i].text = $"{tempPlayerNames} - {tempPlayerScores}";
                }
                done = true;
                // print($"playernames: {tempPlayerNames}");
                // print($"playerScores: {tempPlayerScores}");
                //
                // // playerNames.text = tempPlayerNames;
                // // playerScores.text = tempPlayerScores;
                //
                // //llHighScore
                // print($"Score: {members[0].score}");
                // scores[0].text = members[0].score.ToString();
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
