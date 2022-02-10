using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void select_toggle();

    void drag_end();
    void drag_start();
    void drag_update(Ray r);
}
