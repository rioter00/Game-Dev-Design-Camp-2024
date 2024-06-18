//manages player score in-game
//reference: https://youtu.be/u8llsk7FoYg

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//***
// using LootLocker.Requests;

public class PlayerScoreController : MonoBehaviour
{
    public InGameScore leaderboard; //from score manager object with InGameScore script
    public int score;

    void Start()
    {
        
    }

    void Update()
    {
        //***
        //for testing, can be removed
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(DieRoutine());
        }
        //***
    }

    //when player dies
    IEnumerator DieRoutine()
    {
        //uploads score
        yield return leaderboard.SubmitScoreRoutine(score);
    }
}
