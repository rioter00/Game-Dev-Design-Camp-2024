using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeTwoDirection : PokeFourDirection  
{
    public override IEnumerator ExecuteAttack(float attackTime)
    {
        attacking = true;

        if (myAnim) myAnim.SetTrigger("Attack");

        Vector2 direction;
        if (FollowsMouse) direction = directionFromMouse(false);
        else
        {
            // temp
            direction = Vector2.right;

            if (isEnemy)
            {
                Vector2 value = playerRef.transform.position - attackOffset.position;
                value.Normalize();
                direction = directionFromVector2(false, value);
            }
            else
            {
                direction = directionFromVector2(false, directionFacing);
            }
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

    private void Update()
    {
        if (!attacking && weaponAnchor)
        {
            if (FollowsMouse || isEnemy)
            {
                Vector2 direction;
                if (!isEnemy) direction = directionFromVector2(false, Camera.main.ScreenToWorldPoint(Input.mousePosition) - attackOffset.position);
                else direction = directionFromVector2(false, playerRef.transform.position - attackOffset.position);
                direction.Normalize();

                float rotation = Vector2.Angle(Vector2.right, direction);
                if (direction.y < 0) rotation = -rotation;

                weaponAnchor.transform.localRotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
                // takes direction from player direction
                Vector2 direction = directionFromVector2(false, directionFacing);

                float rotation = Vector2.Angle(Vector2.right, direction);
                if (direction.y < 0) rotation = -rotation;

                weaponAnchor.transform.localRotation = Quaternion.Euler(0, 0, rotation);
            }
        }
    }
}
