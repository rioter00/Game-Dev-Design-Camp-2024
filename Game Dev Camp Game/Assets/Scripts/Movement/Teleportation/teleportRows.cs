using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportRows : MonoBehaviour
{
    [SerializeField] teleportRows teleportMatch;
    [SerializeField] bool canTeleport = true;
    [Header("Tag of object you want to teleport")]
    [SerializeField] string tag;

    // Start is called before the first frame update
    void Start()
    {
        if (teleportMatch == null)
        {
            Debug.Log("You have not matched your teleport columns");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTeleport) return;
        if(collision.CompareTag(tag)){
            var collisionGameObjectobject = collision.gameObject;
            collisionGameObjectobject.transform.position = new Vector2(collisionGameObjectobject.transform.position.x, teleportMatch.gameObject.transform.position.y);
            teleportMatch.disablePortal();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            canTeleport = true;
        }
    }

    void disablePortal()
    {
        canTeleport = false;
    }
}
