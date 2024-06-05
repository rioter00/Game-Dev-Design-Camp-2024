using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportColumns : MonoBehaviour
{
    [SerializeField] teleportColumns teleportMatch;
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
            collisionGameObjectobject.transform.position = new Vector2(teleportMatch.gameObject.transform.position.x, collisionGameObjectobject.transform.position.y);
            teleportMatch.disablePortal();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    void disablePortal()
    {
        canTeleport = false;
    }
}
