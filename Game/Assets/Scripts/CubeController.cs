using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour,IInteractable
{
 
    bool is_selected = false;
    Renderer my_renderer;
    private Vector3 drag_position;
    private Vector3 h;
    private RaycastHit hit;
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
            print("CUBE SELECTED");
            //my_renderer.material.color = Color.red;

        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }

 /**   public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
    }
 **/
    public void drag_start()
    {
       // distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    public void drag_update(Ray r)
    {
       
       // Physics.Raycast() = Vector3.Distance(transform.position, Camera.main.transform.position);
    }
}