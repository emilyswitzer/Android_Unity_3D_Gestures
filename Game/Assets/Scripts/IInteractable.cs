using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void select_toggle(Color color);
    void collision();
}