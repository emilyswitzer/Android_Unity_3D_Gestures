using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour, IInteractable
{
  
    bool is_selected = false;
    Renderer my_renderer;
    private Vector3 drag_position;
    float distance;
    GameObject cameraPlane;

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
            print("SPHERE SELECTED");
          //  my_renderer.material.color = Color.red;

        }
        else
            my_renderer.material.color = Color.white;

    }

    internal void Do_cube_stuff()
    {
        print("Im a cube and Im OK");
    }


    public void drag_start()
    {
        cameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        cameraPlane.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        cameraPlane.transform.up = (Camera.main.transform.position - cameraPlane.transform.position).normalized;
        cameraPlane.GetComponent<Renderer>().enabled = false;
    }

    public void drag_end()
    {
        Destroy(cameraPlane);
    }
    public void drag_update(Ray r)
    {
        RaycastHit info;

        if (Physics.Raycast(r, out info))
        {
         if (info.transform == cameraPlane.transform)
            {
                Vector3 hit = info.point;
                transform.position = hit;
            }
        }
    }
}
