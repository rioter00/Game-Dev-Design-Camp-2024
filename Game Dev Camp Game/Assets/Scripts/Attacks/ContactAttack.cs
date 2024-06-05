using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttack : MonoBehaviour
{
    [Header("Damage attack amount")]
    public int damage = 1;

    [Header("Does this object damage everything it touches or only the player?")]
    public bool onlyDamagePlayer = true;

    [HideInInspector]
    public bool disable = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!disable)
        {
            if ((onlyDamagePlayer && collision.gameObject.GetComponent<PlayerHealth>()) || (!onlyDamagePlayer && collision.gameObject.GetComponent<Health>()))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    void Awake()
    {
        if (damage <= 0)
        {
            Debug.LogWarning(gameObject.name + "'s damage is too low! Defaulting to 1...", gameObject);
            damage = 1;
        }

        if (!GetComponent<Collider2D>())
        {
            Debug.LogError("No collider2D found on " + gameObject.name + "! Add one for this to deal damage.", gameObject);
            enabled = false;
        }
    }
}
