using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTwoDirectionPlatformer : ShootFourDirection
{
    override public IEnumerator ExecuteAttack(float attackTime)
    {
        if (myAmmo)
        {
            if (ShootsAllDirections)
            {
                if (!myAmmo.CheckForAmmo(2)) yield break;
                else myAmmo.UpdateValue(Collectible_Type.Ammo, -2);
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

            for (int i = 0; i <= 180; i = i + 180) // angles: 0, 180
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
                    direction = directionFromMouse(false);
                }
                else
                {
                    //player direction
                    direction = directionFromVector2(false, directionFacing);
                }
            }
            else
            {
                if (FollowsPlayer)
                {
                    // goes towards player
                    direction = directionFromVector2(false, new Vector2(transform.rotation.x, transform.rotation.y));
                }
                else
                {
                    // follows enemy facing direction
                    direction = directionFromVector2(false, directionFacing);
                }
            }

            StartCoroutine(NewMethod(position, direction, 0));
        }


        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    private IEnumerator NewMethod(Vector2 position, Direction direction, float delay)
    {
        yield return new WaitForSeconds(delay);
        // get projectile
        GameObject newProject = projectilePool.pullObject(position);
        if (newProject == null) newProject = Instantiate(projectile, position, transform.rotation);

        if (newProject.GetComponent<ProjectileMove>()) newProject.GetComponent<ProjectileMove>().setDirections(this, projectileSpeed, liveTime, direction, isEnemy);
        else Debug.LogWarning("ProjectileMove component not found on " + projectile.name + ". This object will not move!");

        if (direction == Direction.left) newProject.transform.localRotation = transform.rotation;
        else newProject.transform.localRotation = transform.rotation;
        
        // *** Uncomment for Spread shot, be sure to change projectile layer properties        
        // GameObject newProject1 = projectilePool.pullObject(position);
        // if (newProject1 == null) newProject1 = Instantiate(projectile, position, transform.rotation);

        // if (newProject1.GetComponent<ProjectileMove>()) newProject1.GetComponent<ProjectileMove>().setDirections(this, projectileSpeed, liveTime, direction, isEnemy);
        // else Debug.LogWarning("ProjectileMove component not found on " + projectile.name + ". This object will not move!");

        // if (direction == Direction.left) newProject1.transform.localRotation = transform.rotation;
        // else newProject1.transform.localRotation = transform.rotation;
        
        // newProject1.transform.localRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 4));
    }
}
