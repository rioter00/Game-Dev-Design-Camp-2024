using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMotor_withAcceleration : MonoBehaviour, IMove
{
    Rigidbody2D rb;

    [SerializeField] Animator[] animators;

    IAttack<Health>[] attackScrpits;

    public float moveSpeed = 10f;
    public bool accelerate = false;
    public float accelerationIncrement = 0.0001f;

    void Start() {
        attackScrpits = GetComponents<IAttack<Health>>();
        rb = GetComponent<Rigidbody2D>();
    }
    

    public void Move(Vector2 direction) {
        if (direction.sqrMagnitude < .01f){
            rb.velocity = Vector2.zero;
            UpdateAnimations(direction.x, direction.y);
        }
        else {
            if (accelerate) {
                moveSpeed += (accelerationIncrement * Time.deltaTime);
            }
            rb.velocity = direction.normalized * moveSpeed;
            UpdateAnimations(direction.normalized.x, direction.normalized.y);
        }
    }

    public void UpdateAnimations(float horizontal, float vertical) {
        if (animators.Length > 0) {
            foreach (Animator a in animators) {
                a.SetFloat("HorizontalSpeed", horizontal);
                a.SetFloat("VerticalSpeed", vertical);
            }
        }

        if (attackScrpits.Length > 0)
        {
            foreach (IAttack<Health> attack in attackScrpits)
            {
                attack.SetDirection(new Vector2(horizontal, vertical).normalized);
            }
        }
    }
}
