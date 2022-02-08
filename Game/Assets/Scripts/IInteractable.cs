using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void select_toggle();
   
   // void MoveTo(Vector3 destination);
    void drag_start();
    void drag_update(Ray r);
}
