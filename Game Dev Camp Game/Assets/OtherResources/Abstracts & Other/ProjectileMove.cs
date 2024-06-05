using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ProjectileMove : MonoBehaviour
{
    Attack myScript;
    float speed;
    float life;
    Vector2 direction;
    Direction flatDirection = Direction.none;
    bool dealtDamage = false;
    bool isNew = true;
    bool isLive;
    bool fromEnemy;

    void Update()
    {
        if (isLive)
        {
            switch(flatDirection){

                case Direction.right:
                    {
                        if (Mathf.Abs(direction.x) < 1) direction = new Vector2(1, direction.y);
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Mathf.Abs(direction.x) * 30F, transform.position.y), speed * Time.deltaTime);
                        break;
                    }
                case Direction.left:
                    {
                        if (Mathf.Abs(direction.x) < 1) direction = new Vector2(-1, direction.y);
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-Mathf.Abs(direction.x) * 30F, transform.position.y), speed * Time.deltaTime);
                        break;
                    }
                case Direction.up:
                    {
                        if (Mathf.Abs(direction.y) < 1) direction = new Vector2(direction.x, 1);
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, Mathf.Abs(direction.y) * 30F), speed * Time.deltaTime);
                        break;
                    }
                case Direction.down:
                    {
                        if (Mathf.Abs(direction.y) < 1) direction = new Vector2(direction.x, -1);
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -Mathf.Abs(direction.y) * 30F), speed * Time.deltaTime);
                        break;
                    }
                default:
                    {
                        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
                        break;
                    }
            }
            
            life -= Time.deltaTime;
            if (life < 0)
            {
                isLive = false;
                direction = Vector2.zero;
                if (myScript) myScript.returnToPool(gameObject);
                else Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("projectile collision");
        if(myScript == null)
        {
            return;
        }

        if (!dealtDamage && isLive)
        {
            if ((fromEnemy && collision.GetComponentInParent<PlayerHealth_Expanded>()) || (!fromEnemy && collision.GetComponent<EnemyHealth>()))
            {
                dealtDamage = true;
                myScript?.DealDamage(collision.GetComponentInParent<PlayerHealth_Expanded>());
            }

            if (collision != myScript.GetComponent<Collider2D>() && !collision.CompareTag("CheckPoint"))
            {
                isLive = false;
                direction = Vector2.zero;
                if (myScript) myScript.returnToPool(gameObject);
                else Destroy(gameObject);
            }
            else Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision);
        }
    }

    public void setValues( Attack a, float s, float l, Vector2 d, bool e)
    {
        life = l;
        speed = s;
        direction = d * 30000F;
        isLive = true;
        dealtDamage = false;

        if (isNew)
        {
            isNew = false;
            myScript = a;
            fromEnemy = e;
            GetComponent<Collider2D>().isTrigger = true;
            gameObject.name = "Projectile: " + myScript.ToString();
        }
    }

    public void setDirections(Attack a, float s, float l, Direction d, bool e)
    {
        life = l;
        speed = s;
        flatDirection = d;
        direction = transform.position; //starting point
        isLive = true;
        dealtDamage = false;

        if (isNew)
        {
            isNew = false;
            myScript = a;
            fromEnemy = e;
            GetComponent<Collider2D>().isTrigger = true;
            gameObject.name = "Projectile: " + myScript.ToString();
        }
    }

    public void disableReturn()
    {
        myScript = null;
    }
}