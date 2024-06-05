using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class platformMover : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direction;

    public float moveSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformController.OnPlatformSpeedChange += setSpeed;
        platformController.platformSpeed();
        // Invoke(nameof(platformController.platformSpeed), 2);
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        platformController.OnPlatformSpeedChange -= setSpeed;
    }

    void Move() {
        if (direction.sqrMagnitude < .01f){
            rb.velocity = Vector2.zero;
        }
        else {
            rb.velocity = direction.normalized * moveSpeed;
        }
    }

    void setSpeed(float value)
    {
        moveSpeed = value;
    }
}
