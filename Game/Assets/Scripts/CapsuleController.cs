using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour,IInteractable
{

    bool is_selected = false;
    Renderer my_renderer;
    private Vector3 drag_position;
    float distance;


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
        {
            print("CAPSULE SELECTED");
            // my_renderer.material.color = Color.red;

        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }

    public void drag_end()
    {
        
    }

    public void drag_start()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    public void drag_update(Ray r)
    {
        transform.position = r.GetPoint(distance);
    }
}
