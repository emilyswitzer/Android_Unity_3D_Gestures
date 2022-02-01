using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsukeControlScript : MonoBehaviour,IInteractable
{
    public void select_toggle()
    {
        is_selected = !is_selected;

        if (is_selected)
            my_renderer.material.color = Color.blue;
        else
            my_renderer.material.color = Color.white;
    }

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
}
