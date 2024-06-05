﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeAllDirection : Attack
{
    [Header("How far can the attack hit?")]
    public float attackRange = 1;

    //[Header("Layers that can be hit")]
    public LayerMask attackLayer;

    [Header("Have a gameobject that visually shows the poke?")][Tooltip("To use this, add the WeaponAnchor PREFAB to your object. The sprites are changeable.")]
    public GameObject weaponAnchor;

    override public IEnumerator ExecuteAttack(float attackTime)
    {
        attacking = true;

        if (myAnim) myAnim.SetTrigger("Attack");

        Vector2 direction;
        if (!isEnemy)
        {
            Vector3 mouse = Input.mousePosition;
            direction = (Camera.main.ScreenToWorldPoint(mouse) - attackOffset.position);
            direction.Normalize();
        }
        else
        {
            // how enemy sets direction
            direction = (playerRef.transform.position - attackOffset.position);
            direction.Normalize();
        }
        
        Debug.DrawRay(attackOffset.position, direction * attackRange, Color.red, attackTime);

        if (weaponAnchor)
        {
            float rotation = Vector2.Angle(Vector2.right, direction);
            if (direction.y < 0) rotation = -rotation;
            weaponAnchor.transform.localRotation = Quaternion.Euler(0, 0, rotation);

            weaponAnchor.transform.GetComponentInChildren<Animator>().SetTrigger("Attack");
        }

        yield return new WaitForSeconds(attackTime / 2);
        if (!ShootRaycast(direction, attackRange, attackLayer)) Debug.Log(gameObject.name + " missed their attack!");

        yield return new WaitForSeconds(attackTime / 2);
        attacking = false;
    }

    override protected void Awake()
    {
        base.Awake();

        if (attackRange <= 0)
        {
            Debug.LogWarning(gameObject.name + "'s attack range is too small! Defaulting to 1...");
            attackRange = 1;
        }

        if (weaponAnchor)
        {
            if(attackSpeed != 1)
            {
                //int newFrameRate = Mathf.RoundToInt((1F / attackSpeed) * 8F);
                //weaponAnchor.transform.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips[1].frameRate = newFrameRate;
                weaponAnchor.transform.GetComponentInChildren<Animator>().speed = 1F / attackSpeed;
            }
        }
        if (isEnemy)
        {
            attackLayer = LayerMask.GetMask("Default");
        }
        else
        {
            attackLayer.value = LayerMask.GetMask("Enemy");
        }
    }

    private void Update()
    {
        if (!attacking && weaponAnchor)
        {
            Vector2 direction;
            if(!isEnemy) direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - attackOffset.position);
            else direction = (playerRef.transform.position - attackOffset.position);
            direction.Normalize();

            float rotation = Vector2.Angle(Vector2.right, direction);
            if (direction.y < 0) rotation = -rotation;

            weaponAnchor.transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
