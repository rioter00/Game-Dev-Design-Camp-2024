using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Drag this on any object that you want make a sound, can be a repeating sound

public class essenceAudio : MonoBehaviour
{
    [Header("Do you want sound to play whenever this object is in the game? Add Audio Clip below")]
    public AudioClip essenseSound;
    [Header("Adjust the volume of that sound")]
    public float soundVolume = 1f;
    [Header("How often do you want the sound to repeat (in Seconds)?", order=0), Space(10), Header("Use '0' if never repeat", order=1)]
    [Range(0f, 30f)]
    public float repeatInterval;
    public float pitchVariance;

    private void Start()
    {
        if(repeatInterval == 0)
        {
            playEssense();
        } else
        {
            InvokeRepeating("playEssense", 0, repeatInterval);
        }
    }

    private void OnEnable()
    {
        Start();
    }

    private void playEssense()
    {
        if (essenseSound == null)
        {
            return;
        }
        else
        {
            AudioManager.audioManager.playAudio(essenseSound, soundVolume);
        }
    }
}
