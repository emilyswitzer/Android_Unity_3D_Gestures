using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**This is the drag along the vertical plane **/
public class SphereController : MonoBehaviour, IInteractable
{
  
    bool is_selected = false;
    Renderer my_renderer;
    GameObject camera_plane;

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
             my_renderer.material.color = Color.red;


        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("sphere selected");
    }


    public void drag_start()
    {
        camera_plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        camera_plane.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        camera_plane.transform.localScale = new Vector3(4, 4, 4);
        camera_plane.transform.up = (Camera.main.transform.position - camera_plane.transform.position).normalized;
        camera_plane.GetComponent<Renderer>().enabled = false;
    }

    public void drag_end()
    {
        Destroy(camera_plane);
       

    }
    public void drag_update(Ray r)
    {
        RaycastHit info;

        if (Physics.Raycast(r, out info))
        {
         if (info.transform == camera_plane.transform)
            {
                Vector3 hit = info.point;
                transform.position = hit;
            }
        }
    }
}
