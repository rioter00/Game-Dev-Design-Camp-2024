using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move(Vector2 direction);

    void UpdateAnimations(float horizontal, float vertical);
}
