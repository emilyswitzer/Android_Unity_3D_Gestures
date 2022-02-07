using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour,IInteractable
{

    [SerializeField, SerializeReference]
    bool is_selected = false;
    Renderer my_renderer;
    private Vector3 drag_position;

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
        print("Im a cube and Im OK");
    }

    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
    }

   
}
