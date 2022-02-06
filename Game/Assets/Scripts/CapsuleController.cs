using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour,IInteractable
{
    Renderer my_renderer;

    // Start is called before the first frame update
    void Start()
    {

        my_renderer = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {



    }

    public void collision()
    {
        print("COLLIDED");
    }
    //This is to change colour when selected - this needs to be automatically switched off when deselected
    public void select_toggle(Color color)
    {
      
            my_renderer.material.SetColor("_Color", color);


    }
   
}
