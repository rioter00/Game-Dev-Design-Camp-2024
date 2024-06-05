﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerMovement_withSlide : MonoBehaviour, IMove
{
    Rigidbody2D rb;
    [SerializeField] Animator[] animators;

    IAttack<Health>[] attackScripts;

    public float moveSpeed = 10f;
    [Header("This behaves like 'PlatformerMovement.cs' but character will not autostop when you let go of direction")]
    [Tooltip("This behaves like 'PlatformerMovement.cs' but character will not autostop when you let go of direction")]
    [SerializeField] bool slide = false;
    [SerializeField] float slideDissaption = 0;

    void Start(){
        attackScripts = GetComponents<IAttack<Health>>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > .01f)
        {
            rb.velocity = new Vector2(moveSpeed * direction.x, rb.velocity.y);
        }
        else {
            if (slide)
            {
                //rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        UpdateAnimations(rb.velocity.normalized.x, rb.velocity.normalized.y); //jump animations set in jump script
    }

    public void UpdateAnimations(float horizontal, float vertical) {
        if (animators.Length > 0){
            foreach (Animator a in animators) {
                a.SetFloat("HorizontalSpeed", horizontal);
                a.SetFloat("VerticalSpeed", vertical);
            }
        }

        if (attackScripts.Length <= 0) return;
        foreach (IAttack<Health> attack in attackScripts)
        {
            attack.SetDirection(new Vector2(horizontal, vertical).normalized);
        }
    }

}
