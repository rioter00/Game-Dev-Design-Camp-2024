using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    public delegate void PlatformControl(float speed);
    public static event PlatformControl OnPlatformSpeedChange;

    public delegate void PlatformDelegate();
    public static PlatformDelegate platformSpeed;
        
    public float initialChangeDelay = 60;
    public float subsequentDelay = 45;
    public float currentDelay = 0;
    public float speedChangeIncrement = 4;
    private bool initialChangeMet;
    public float currentTime = 0;
    public float speed = .5f;

    // Start is called before the first frame update

    private void Awake()
    {
        platformSpeed = SpeedRequest;
    }

    void Start()
    {
        currentDelay = initialChangeDelay;
        Invoke("initialSpeed", 1);
        
    }

    void initialSpeed()
    {
        if (OnPlatformSpeedChange != null)
        {
            OnPlatformSpeedChange(speed);
        }
    }

    public void SpeedRequest()
    {
        initialSpeed();
    }
    
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if ( currentTime > currentDelay)
        {
            speed += speedChangeIncrement;
            if (OnPlatformSpeedChange != null)
            {
                OnPlatformSpeedChange(speed);
            }
            currentTime = 0;
            currentDelay = subsequentDelay;
        }
    }
}