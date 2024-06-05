using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAllDirection : Attack
{
    [Header("PREFAB that this object shoots")]
    public GameObject projectile;

    [Header("How fast does the projectile move?")]
    public float projectileSpeed = 5;

    [Header("How long will this projectile live until it disappears?")]
    public float liveTime = 10;

    [Header("Can you shoot endlessly or need to pick up more ammo? (PLAYER ONLY)")][Tooltip("If false, make sure CollectibleManager is on the player")]
    public bool infiniteAmmo = true;
    protected CollectibleManager myAmmo;

    protected ObjectPool projectilePool;

    override public IEnumerator ExecuteAttack(float attackTime)
    {
        if (myAmmo)
        {
            if (!myAmmo.CheckForAmmo(1)) yield break;
            else myAmmo.UpdateValue(Collectible_Type.Ammo, -1);
        }

        attacking = true;

        // get angle
        Vector2 direction;
        if (!isEnemy)
        {
            Vector3 mouse = Input.mousePosition;
            direction = (Camera.main.ScreenToWorldPoint(mouse) - attackOffset.transform.position);
            direction.Normalize();
        }
        else
        {
            // some targetting thing
            direction = (playerRef.transform.position - attackOffset.transform.position);
            direction.Normalize();
        }
        float rotation = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0) rotation = -rotation;

        if (myAnim) myAnim.SetTrigger("Attack");

        // get projectile
        GameObject newProject = projectilePool.pullObject(attackOffset.transform.position);
        if (newProject == null)
        {
            newProject = Instantiate(projectile, attackOffset.transform.position, Quaternion.identity);
            if (newProject.GetComponent<ProjectileMove>()) projectilePool.addToAll(newProject.GetComponent<ProjectileMove>());
            else
            {
                Debug.LogError("Projectile from " + gameObject.name + " does not have ProjectileMove!");
                attacking = false;
                yield break;
            }
        }

        if (newProject.GetComponent<ProjectileMove>()) newProject.GetComponent<ProjectileMove>().setValues(this, projectileSpeed, liveTime, direction, isEnemy);
        else Debug.LogWarning("ProjectileMove component not found on " + projectile.name + ". This object will not move!");

        newProject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation));

        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    override protected void Awake()
    {
        base.Awake();

        if (!projectile)
        {
            Debug.LogError("No projectile set. " + gameObject.name + " cannot shoot!", gameObject);
            this.enabled = false;
        }
        else
        {
            projectilePool = new ObjectPool();
        }

        if(projectileSpeed <= 0)
        {
            Debug.LogWarning(gameObject.name + "'s shoot speed is too low! Defaulting to 1...", gameObject);
            projectileSpeed = 1;
        }

        if(!infiniteAmmo)
        {
            if (isEnemy)
            {
                Debug.Log(gameObject.name + " cannot use finite ammo! Setting infiniteAmmo to true...");
                infiniteAmmo = true;
            }

            myAmmo = GetComponent<CollectibleManager>();
            if(!myAmmo)
            {
                Debug.LogError(gameObject.name + " cannot have finite ammo without a CollectableManager component! Turning on infinite ammo...", gameObject);
                infiniteAmmo = true;
            }
        }

        if(liveTime <= 0)
        {
            Debug.LogWarning(gameObject.name + "'s bullets won't live long enough to hit anything! Defaulting liveTime to 1...", gameObject);
            liveTime = 1;
        }
    }

    override public void returnToPool(GameObject obj)
    {
        if (obj) projectilePool.returnObject(obj);
    }

    public void disablePool()
    {
        projectilePool.objectDestroyed();
    }
}
