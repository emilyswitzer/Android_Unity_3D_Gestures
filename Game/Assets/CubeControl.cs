using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour,IInteractable
{
    Renderer my_renderer;
    bool is_selected = false;

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
    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
            my_renderer.material.color = Color.red;
        else
            my_renderer.material.color = Color.white;

    }
    public void changeColour()
    {
        selectedObjectColour.material.SetColor("_Color", color);

    }

  
    
}