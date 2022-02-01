using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTouchManagerScript : MonoBehaviour,ITouchController
{

    IInteractable selected_object;
    public void drag(Vector2 current_position)
    {
        throw new System.NotImplementedException();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
