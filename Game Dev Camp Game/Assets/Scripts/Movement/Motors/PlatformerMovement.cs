using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerMovement : MonoBehaviour, IMove
{
    Rigidbody2D rb;
    [SerializeField] Animator[] animators;

    IAttack<Health>[] attackScripts;

    public float moveSpeed = 10f;
    private float lastHorizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        attackScripts = GetComponents<IAttack<Health>>();
    }


    public void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > .01f)
        {
            rb.velocity = new Vector2(moveSpeed * direction.x, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(0f, rb.velocity.y);
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

        // if (Mathf.Abs(horizontal) > 0)
        // {
        //     lastHorizontal = horizontal;
        // }

        // if (attackScripts.Length > 0)
        // {
        //     foreach (IAttack<Health> attack in attackScripts)
        //     {
        //         attack.SetDirection(new Vector2(lastHorizontal, vertical).normalized);
        //     }
        // }
    }
}
