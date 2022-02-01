using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControlScript : MonoBehaviour
{
    Renderer my_renderer;
    bool is_selected = false;
    IInteractable selected_object;
    // Start is called before the first frame update
    void Start()
    {
        my_renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        is_selected = !is_selected;

        if (is_selected)
            my_renderer.material.color = Color.yellow;
        else
            my_renderer.material.color = Color.white;

    }
}
