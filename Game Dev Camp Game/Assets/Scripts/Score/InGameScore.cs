//reference: https://youtu.be/u8llsk7FoYg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//***
// using LootLocker.Requests;
using TMPro;

public class InGameScore : MonoBehaviour
{

    [SerializeField] private KeyCode updateScoreButton;
    //
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private bool scoreStarted;
    [SerializeField] private float runningScore;  
    //
    [SerializeField] private float llHighScore;

    //Brian added
    int leaderboardID = 8390;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI runningScoreText;
    [SerializeField] private int collectibleScore = 100;

    public static InGameScore instance;

    // Start is called before the first frame update
    void Start()
    {
        //getLootLockerHighscore(); //taken care of in FetchTopHighscoresRoutine()
        //Debug.Log(llHighScore); //this will not work in one scene in Start() as there must be a delay for LootLocker to load upon starting the game

        CollectibleManager.OnCollectibleScore += AddCollectibleScore;
        instance = this;
    }

    private void OnDestroy()
    {
        // StartCoroutine(updateLootLockerScore());
        CollectibleManager.OnCollectibleScore -= AddCollectibleScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreStarted)
        {
            runningScore += (scoreMultiplier * Time.deltaTime);
            runningScoreText.text = Mathf.Floor(runningScore).ToString();
        }
        
        if (Input.GetKeyDown(updateScoreButton))
        {
            // StartCoroutine(updateLootLockerScore());
        }
    }

    public void SubmitScore()
    {
        // StartCoroutine(updateLootLockerScore());
    } 

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        // LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        // {
        //     if (response.success)
        //     {
        //         Debug.Log("Successfully uploaded score");
        //         done = true;
        //     }
        //     else
        //     {
        //         Debug.Log("Failed" + response.Error);
        //         done = true;
        //     }
        // });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator updateLootLockerScore()
    {
        yield return this.SubmitScoreRoutine((int)runningScore);
    }

    //used to grab multiple highscores
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        // LootLockerSDKManager.GetScoreList(leaderboardID, 1, 0, (response) => //the second int (1) indicates how many top scoring places to be displayed while the third int (0) displays the bottom scores
        // {
        //     if (response.success)
        //     {
        //         string tempPlayerNames = "";
        //         string tempPlayerScores = "";
        //
        //         LootLockerLeaderboardMember[] members = response.items;
        //
        //         for (int i = 0; i < members.Length; i++)
        //         {
        //             if (members[i].player.name != "")
        //             {
        //                 tempPlayerNames += members[i].player.name;
        //             }
        //             else
        //             {
        //                 tempPlayerNames += members[i].player.id;
        //             }
        //             tempPlayerScores = members[i].score.ToString();
        //             // tempPlayerNames += "\n";
        //         }
        //         done = true;
        //         // playerNames.text = tempPlayerNames;
        //         highScore.text = tempPlayerScores;
        //
        //         //llHighScore
        //         llHighScore = members[0].score;
        //     }
        //     else
        //     {
        //         Debug.Log("Failed" + response.Error);
        //         done = true;
        //     }
        // });
        yield return new WaitWhile(() => done == false);
    }

    //if the player touches a collectible, the collectibleScore gets added to their total score (runningScore)
    private void AddCollectibleScore(int value)
    {
        runningScore += value;
    }

    //taken care of in FetchTopHighscoresRoutine()
    /*void getLootLockerHighscore()
    {
        // grab the highest score on loot locker (llHighScore)
        
        // brian's code
    }*/
}
