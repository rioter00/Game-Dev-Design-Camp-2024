using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPointMotor : MonoBehaviour, IMove
{
    Rigidbody2D rb;
    Vector2 destination;
    [SerializeField] Animator[] animators;

    IAttack<Health>[] attackScrpits;

    public float moveSpeed = 10f;



    void Start()
    {
        destination = transform.position;
        attackScrpits = GetComponents<IAttack<Health>>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
        if (direction.sqrMagnitude > .01f) {
            rb.velocity = direction.normalized * moveSpeed;
            UpdateAnimations(direction.normalized.x, direction.normalized.y);
        }
        else {
            rb.velocity = Vector2.zero;
            UpdateAnimations(0, 0);
        }
    }


    public void Move(Vector2 position){
        destination = position;
    }

    public void UpdateAnimations(float horizontal, float vertical) {
        if (animators.Length > 0){
            foreach (Animator a in animators){
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
