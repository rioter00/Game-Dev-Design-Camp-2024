using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(IMove))]
public class PlayerInputController : MonoBehaviour
{

    IMove motor;
    IJump jumpMotor;
    private Vector2 direction;

    void Start(){
        motor = GetComponent<IMove>();
        jumpMotor = GetComponent<IJump>();
    }

    void Update(){
        if (motor != null)
        {
            // print(motor.Mo);
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            motor.Move(direction);
            
            
            // motor.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }

        if (jumpMotor != null) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                jumpMotor.Jump();
            }
        }
        
    }
}
