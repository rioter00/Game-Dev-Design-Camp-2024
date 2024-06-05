﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Put the sprite for when the object is turned OFF here")]
    public Sprite OffSprite;
    [Header("Put the sprite for when the object is turned ON here")]
    public Sprite OnSprite;

    private SpriteRenderer spriteRenderer;

    private bool value;

    [Header("What key should the player press to use this interactable?")]
    public KeyCode keycode;

    [Header("Is the interactable turned on?")]
    public bool active;


    [Header("Are the target objects starting in the opposite state (active/inactive)", order = 0)]
    [Space(-10, order = 1)]
    [Header("as this interactiveble? Check for oppposite states, ", order = 2)]
    [Space(-10, order = 3)]
    [Header("uncheck for same states.", order = 4)]
    [Space(10, order = 5)]

    public bool oppositeState;

    [Header("How many objects would you like this interactable to turn on? Drag in the objects")]
    public GameObject[] target;
    private bool triggerEntered;

    [Header("Do you want to play an sound when interacting? Add an audio clip")]
    public AudioClip sound;
    public float soundVolume = 1f;

    [Header("Does this require a key?")]
    public bool requiresKey;
    private bool hasKey;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (!active)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (oppositeState)
                {
                    target[i].SetActive(active);
                } else
                {
                    target[i].SetActive(false);
                }
            }
        }
        else if (active)
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode) && triggerEntered == true)
        {
            interact();
            Change();
        }
    }

    public bool isInteractable()
    {
        return true;
    }

    public void interact()
    {
        // trigger audio event
        if(sound !=null)
        AudioManager.audioManager.playAudio(sound, soundVolume);

        if (requiresKey)
        {
            if (hasKey)
            {
                if (active)
                {
                    Debug.Log("Active");

                    for (int i = 0; i < target.Length; i++)
                    {
                        if (oppositeState)
                        {
                            target[i].SetActive(true);
                        }
                        else
                        {
                            target[i].SetActive(false);
                        }
                    }
                    active = false;
                }
                else
                {
                    Debug.Log("is interactable");
                    for (int i = 0; i < target.Length; i++)
                    {

                        if (oppositeState)
                        {
                            target[i].SetActive(false);
                        }
                        else
                        {
                            target[i].SetActive(true);
                        }
                    }
                    active = true;
                    player.GetComponent<CollectibleManager>().UseKey();
                }
            }
        }
        else if(!requiresKey)
        {
            if (active)
            {
                Debug.Log("Active");

                for (int i = 0; i < target.Length; i++)
                {
                    target[i].SetActive(false);
                }
                active = false;
            }
            else
            {
                Debug.Log("is interactable");
                for (int i = 0; i < target.Length; i++)
                {
                    target[i].SetActive(true);
                }
                active = true;
            }
        }
    }
    public bool isActive()
    {
        return active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            triggerEntered = true;
            player = other.gameObject;

            if (requiresKey && other.gameObject.GetComponent<CollectibleManager>().keyCollected)
            {
                hasKey = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) triggerEntered = false;
    }

    public void Change()
    {
        if (requiresKey)
        {
            if (hasKey)
            {
                if (spriteRenderer.sprite == OnSprite)
                    spriteRenderer.sprite = OffSprite;
                else
                {
                    spriteRenderer.sprite = OnSprite;
                }
            }
        }
        else if (!requiresKey)
        {
            if (spriteRenderer.sprite == OnSprite)
                spriteRenderer.sprite = OffSprite;
            else
            {
                spriteRenderer.sprite = OnSprite;
            }
        }
    }
}
