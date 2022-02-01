using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTouchManagerScript : MonoBehaviour, ITouchController
{
    private float tap_timer;
    private bool has_moved;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    IInteractable selected_object;
    ITouchController[] managers;
    public void drag(Vector2 current_position)
    {
        //raycast the position
        print(current_position);
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

            the_object.select_toggle();
            selected_object = the_object;


            if (the_object is CubeControl)
            { (the_object as CubeControl).do_cube_stuff(); }
            print("I hit something!!");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //  managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();

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
                    tap_timer = 0f;
                    has_moved = false;

                    break;
                case TouchPhase.Stationary:


                    break;
                case TouchPhase.Moved:
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
