using System.Collections;
using System.Collections.Generic;

public interface IInteractable
{
    bool isActive();

    bool isInteractable(); //Bool for object being interactable

    void interact(); //parameteres for interactable objects


    //bool isActive(); //if the interactable object has two states
}

