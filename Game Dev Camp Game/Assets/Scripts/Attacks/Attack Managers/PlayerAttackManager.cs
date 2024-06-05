using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    /// This manages cooldowns/damage for any attacks player might have; it is required for ANY attacking

    // list of attacks on the player; set on start
    [HideInInspector]
    public Attack[] playerAttacks;

    [Header("Disables all attacks on player")]
    public bool attacksEnabled = true;

    void Start()
    {
        playerAttacks = gameObject.GetComponents<Attack>();
        if(playerAttacks == null || playerAttacks.Length < 1)
        {
            Debug.LogError("No attacks found on " + gameObject.name + "!" + gameObject.name + " is unable to attack.");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (attacksEnabled && !GetComponent<PlayerHealth>().dead)
        {
            foreach (Attack a in playerAttacks) // I don't like this here
            {
                if (a.enabled)
                {
                    if (!a.attacking && Input.GetKeyDown(a.attackInput))
                    {
                        StartCoroutine(a.ExecuteAttack(a.attackSpeed));
                    }
                }
            }
        }
    }
}
