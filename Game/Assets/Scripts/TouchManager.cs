using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TouchManager : MonoBehaviour, ITouchController
{
    IInteractable selected_object;
    bool is_dragging = false;
    private float tap_timer;
    private bool has_moved;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    ITouchController[] managers;
    float touchDist = 0;
    float lastDist = 0;

    
    public void drag(Vector2 current_position)
    {

        Ray ourRay = Camera.main.ScreenPointToRay(current_position);

        Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);


        if (selected_object != null)
        {

            if (!is_dragging)
            {
                selected_object.drag_start();
                is_dragging = true;
                
            }

            selected_object.drag_update(ourRay);
           
            if (is_dragging && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                is_dragging = false;
            }


        }
    }

    public void drag_ended()
    {
        is_dragging = false;
        if (selected_object != null)
        {
            selected_object.drag_end();
        }
            
    }

    public void pinch(Vector2 position_1, Vector2 position_2, float relative_distance)
    {

        Ray our_ray = Camera.main.ScreenPointToRay(position_2 - position_1);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.green, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();

            print("hi");
        }

    }

    public void tap(Vector2 position)
    {

        Ray our_ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.green, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();

            if (selected_object != null)
                selected_object.select_toggle();

            the_object.select_toggle();
            selected_object = the_object;


            if (the_object is CubeController)
            { (the_object as CubeController).Do_cube_stuff(); }



            else if (the_object is SphereController)
            { (the_object as CubeController).Do_cube_stuff(); }


            else if (the_object is CapsuleController)
            {
                (the_object as CubeController).Do_cube_stuff();
            }

            else
            {
                selected_object.select_toggle();
                selected_object = null;
            }
        }
    }
        // Start is called before the first frame update
        void Start()
        {
            managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            tap_timer += Time.deltaTime;
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            
            switch (first_touch.phase)
            {
                case TouchPhase.Began:
                    tap_timer = 0f;
                    has_moved = false;

                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
                    has_moved = true;


                    if (has_moved == true)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).drag(first_touch.position);

                    }
                    break;

                case TouchPhase.Ended:
                    if ((tap_timer < MAX_ALLOWED_TAP_TIME) && !has_moved)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).tap(first_touch.position);
                    }
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).drag_ended();
                    break;

            }



        }
       else if (Input.touchCount == 2)
        {
            tap_timer += Time.deltaTime;
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            Touch second_touch = all_touches[1];

            if (first_touch.phase == TouchPhase.Began && second_touch.phase == TouchPhase.Began)
            {
                lastDist = Vector2.Distance(first_touch.position, second_touch.position);
            }

            if (first_touch.phase == TouchPhase.Moved && second_touch.phase == TouchPhase.Moved)
            {
                float newDist = Vector2.Distance(first_touch.position, second_touch.position);
                touchDist = lastDist - newDist;
                lastDist = newDist;

              
                Camera.main.fieldOfView += touchDist * 0.1f;
            }
        }




    }
 


}
