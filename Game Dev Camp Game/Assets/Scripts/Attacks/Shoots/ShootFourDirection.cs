using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFourDirection : ShootAllDirection
{
    [Header("Shoots towards the player?")][Tooltip("Player:\ntrue = Shoots the direction player is facing\nfalse = shoots direction closest to mouse\nEnemy:\ntrue = Will always shoot direction of player\nfalse = Shoots direction they are facing")]
    public bool FollowsPlayer = false;

    [Header("Shoot all directions or only one?")][Tooltip("If ShootFourDirection, turning this true will shoot four directions when triggered\nIf ShootTwoDirection, turning this true will shoot two directions when triggered")]
    public bool ShootsAllDirections = false;

    override public IEnumerator ExecuteAttack(float attackTime)
    {
        if (myAmmo)
        {
            if (ShootsAllDirections)
            {
                if (!myAmmo.CheckForAmmo(4)) yield break;
                else myAmmo.UpdateValue(Collectible_Type.Ammo, -4);
            }
            else 
            {
                if (!myAmmo.CheckForAmmo(1)) yield break;
                else myAmmo.UpdateValue(Collectible_Type.Ammo, -1); 
            }
        }

        attacking = true;

        if (myAnim) myAnim.SetTrigger("Attack");

        if (ShootsAllDirections)
        {
            Vector2 position = transform.position;
            if (attackOffset) position = attackOffset.position;

            for(int i = -90; i <= 180; i = i + 90) // angles: -90, 0, 90, 180
            {
                GameObject newProject = projectilePool.pullObject(position);
                if (newProject == null) newProject = Instantiate(projectile, position, Quaternion.identity);

                if (newProject.GetComponent<ProjectileMove>()) newProject.GetComponent<ProjectileMove>().setDirections(this, projectileSpeed, liveTime, getDirectionFromAngle(i), isEnemy);
                else Debug.LogWarning("ProjectileMove component not found on " + projectile.name + ". This object will not move!");

                newProject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, i));
            }
        }

        else
        {
            Vector2 position = transform.position;
            if (attackOffset) position = attackOffset.position;

            // get angle
            Direction direction;
            if (!isEnemy)
            {
                if (!FollowsPlayer)
                {
                    //follows down
                    direction = directionFromMouse(true);
                }
                else
                {
                    //player direction  
                    direction = directionFromVector2(true, directionFacing);
                }
            }
            else
            {
                if (FollowsPlayer)
                {
                    // goes towards player
                    direction = directionFromVector2(true, playerRef.transform.position - new Vector3(position.x, position.y, 0));
                }
                else
                {
                    // follows enemy facing direction
                    direction = directionFromVector2(true, directionFacing);
                }
            }
            float rotation = 0;
            if (direction == Direction.right) rotation = 0;
            else if (direction == Direction.left) rotation = 180;
            else if (direction == Direction.up) rotation = 90;
            else if (direction == Direction.down) rotation = -90;

            // get projectile
            GameObject newProject = projectilePool.pullObject(position);
            if (newProject == null) newProject = Instantiate(projectile, position, Quaternion.identity);

            if (newProject.GetComponent<ProjectileMove>()) newProject.GetComponent<ProjectileMove>().setDirections(this, projectileSpeed, liveTime, direction, isEnemy);
            else Debug.LogWarning("ProjectileMove component not found on " + projectile.name + ". This object will not move!");

            newProject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        }


        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    protected Direction directionFromMouse(bool fourDirections)
    {
        Vector3 mouse = Input.mousePosition;
        Vector2 mousePos = (Camera.main.ScreenToWorldPoint(mouse) - transform.position);
        if(attackOffset) mousePos = (Camera.main.ScreenToWorldPoint(mouse) - attackOffset.position);
        mousePos.Normalize();

        return directionFromVector2(fourDirections, mousePos);
    }

    protected Direction directionFromVector2(bool fourDirections, Vector2 direct)
    {
        /// Takes a position and returns direction closest to it
        /// fourDirections = true;      returns Direction.left/right/up/down
        /// fourDirections = false;     returns Direction.left/right

        if (!fourDirections || Mathf.Abs(direct.x) >= Mathf.Abs(direct.y))
        {
            // left or right
            if (direct.x > 0) return Direction.right;
            else return Direction.left;
        }
        else
        {
            // up or down
            if (direct.y > 0) return Direction.up;
            else return Direction.down;
        }
    }

    protected Direction getDirectionFromAngle(int angle)
    {
        switch(angle)
        {
            case -90: return Direction.down;
            case 0: return Direction.right;
            case 90: return Direction.up;
            case 180: return Direction.left;
            default: return Direction.none;
        }
    }
}
