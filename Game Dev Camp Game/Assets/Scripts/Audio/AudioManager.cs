using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    AudioSource eventAudioSource;
    AudioSource soundtrackAudioSource;

    // -------- SOUND TRACK IS THROUGH THE SOUNDTRACK.CS (CHILD OF AUDIO MANAGER PREFAB)

    public void Awake()
    {
        if(audioManager != null)
        {
            Destroy(gameObject);
        } else
        {
            audioManager = this;
        }

        eventAudioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// Use AudioManager.audioManager.playAudio(sound, soundVolume);
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public void playAudio(AudioClip clip, float volume)
    {
        if(eventAudioSource == null)
        {
            return;
        }
        else
        {
            eventAudioSource.PlayOneShot(clip, volume);
        }
        
    }

    public void playAudio(AudioClip clip, float volume, float pitchVariance)
    {
        if (eventAudioSource == null)
        {
            return;
        }
        else
        {
            var pitch = 1-Random.Range(-pitchVariance, pitchVariance);
            eventAudioSource.pitch = pitch;
            eventAudioSource.PlayOneShot(clip, volume);
            eventAudioSource.pitch = 1;
        }
    }
}
