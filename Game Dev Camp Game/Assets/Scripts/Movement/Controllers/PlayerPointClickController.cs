using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointClickController : MonoBehaviour
{
    IMove motor;

    void Start()
    {
        motor = GetComponent<IMove>();
    }

    void Update(){

        if (Input.GetMouseButtonDown(1)) {
            if (motor != null){
                Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                motor.Move(new Vector2(v3.x, v3.y));
            }
        }
    }
}
