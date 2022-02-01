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

    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
            my_renderer.material.color = Color.red;
        else
            my_renderer.material.color = Color.white;

    }

    internal void do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }
}