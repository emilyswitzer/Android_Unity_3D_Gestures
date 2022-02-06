using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TouchManager : MonoBehaviour, ITouchController
{
    private float tap_timer;
    private bool has_moved = false;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    IInteractable selected_object;
    private float dist;
    public Vector2 startPos;
    private Vector2 direction;
    ITouchController[] managers;
    public void drag(Vector2 current_position)
    {
        print("Im the manager and I recieved a drag from gesture");

        Ray our_ray = Camera.main.ScreenPointToRay(current_position);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.red, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();
            selected_object = the_object;

            if (the_object is CubeController)
            {
     
                the_object.select_toggle(Color.cyan);

            }
            else if (the_object is SphereController)
            {
                the_object.select_toggle(Color.magenta);

            }

            else if (the_object is CapsuleController)
            {
                the_object.select_toggle(Color.yellow);
            }

        }
    }

    public void pinch(Vector2 position_1, Vector2 position_2, float relative_distance)
    {
        throw new System.NotImplementedException();
    }

    public void tap(Vector2 position)
    {
        print("Im the manager and I recieved a tap from gesture");

        Ray our_ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(our_ray.origin, our_ray.direction * 50, Color.red, 4f);
        RaycastHit hit_info;
        if (Physics.Raycast(our_ray, out hit_info))
        {
            IInteractable the_object = hit_info.transform.GetComponent<IInteractable>();
          //  the_object.select_toggle();
            selected_object = the_object;



            if (the_object is CubeController)
            {
                the_object.select_toggle(Color.red);

            }
            else if (the_object is SphereController)
            {
                the_object.select_toggle(Color.blue);

            }

            else if (the_object is CapsuleController)
            {
                the_object.select_toggle(Color.green);
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
        if (Input.touchCount > 0)
        {

            tap_timer += Time.deltaTime;
            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            print(first_touch.phase);

            switch (first_touch.phase)
            {
                case TouchPhase.Began:
                   startPos = first_touch.position;
                    tap_timer = 0f;
                    has_moved = false;  
                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
                    direction = first_touch.position - startPos;
                    has_moved = true;
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).drag(first_touch.position);
                    break;

                case TouchPhase.Ended:
                    if ((tap_timer < MAX_ALLOWED_TAP_TIME) && !has_moved)
                    {
                        foreach (ITouchController manager in managers)
                            (manager as ITouchController).tap(first_touch.position);
                    }
                    break;

            }

        }


    }
}
