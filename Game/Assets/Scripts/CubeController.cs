using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**This is the drag along the ground plane **/
public class CubeController : MonoBehaviour, IInteractable
{
 
    bool is_selected = false;
    Renderer my_renderer;
    LayerMask plane;

    // Start is called before the first frame update
    void Start()
    {

        my_renderer = GetComponent<Renderer>();
        plane = LayerMask.GetMask("Plane");

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
           my_renderer.material.color = Color.red;

        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("cube selected");
    }

    public void drag_end()
    {

    }

    

    public void drag_start()
    {
        
    }

    public void drag_update(Ray r)
    {
        RaycastHit info;

        if (Physics.Raycast(r, out info, 100.0f, plane))
        {

            if (info.transform && is_selected == true)
            {

                transform.position = info.point + info.normal * 0.5f;
            }
        }

    }
}