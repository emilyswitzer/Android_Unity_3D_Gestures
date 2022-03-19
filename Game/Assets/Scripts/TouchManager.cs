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
    private bool cam_moved;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    ITouchController[] managers;
    GameObject ourCameraPlane;
    Quaternion startOrientation;
    Vector3 scale;
    float startDistance;
    float startAngle;
    Vector3 originalCamPosition;
    Quaternion originalCamRotation;

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

        }

    }

    public void startRotateScale()
    {
        if (selected_object != null)
        {
            startOrientation = selected_object.gameObject.transform.rotation;
            scale = selected_object.gameObject.transform.localScale;
        }

        else
        {
            startOrientation = Camera.main.transform.rotation;
            scale = Camera.main.transform.localScale;
        }

    }

    public void rotateScale(float rotationDegrees, float endDistance, float diff)
    {
        if (selected_object == null)
        {
            if (Quaternion.Angle(startOrientation, startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward)) > 15)
            {
                print("Rotate camera");
                Camera.main.transform.rotation = startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward);
            }

            else
            {
                print("Scale camera");
                diff = endDistance - startDistance;
                Camera.main.transform.position += (diff / 1000) * transform.forward;
            }
        }

        else
        {
            if (Quaternion.Angle(startOrientation, startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward)) > 15)
            {
                print("Rotate object");
                selected_object.gameObject.transform.rotation = startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward);
            }

            else
            {
                print("Scale Object");
                selected_object.gameObject.transform.localScale = scale * diff;
            }
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

            selected_object = the_object;
            the_object.select_toggle();
            


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


    public void verticalDrag(float touchPos)
    {
        Touch touch = Input.touches[0];
        Touch touchOne = Input.touches[1];

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled
        || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
        {
            return;
        }
        if (touch.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
        {

        }
        else
        {
            print("Veritcal drag camera");
            Camera.main.transform.eulerAngles += new Vector3((touch.deltaPosition.y + touchOne.deltaPosition.y) / 5, (touch.deltaPosition.x + touchOne.deltaPosition.x) / 5, 0);
        }
    }

    public void moveCam(float pinchDistance)
    {
        print("Move camera");
        Camera.main.transform.position += Vector3.forward * (pinchDistance) * 0.1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        managers = FindObjectsOfType<MonoBehaviour>().OfType<ITouchController>().ToArray();
        ourCameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ourCameraPlane.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
      

        ourCameraPlane.transform.eulerAngles = new Vector3(ourCameraPlane.transform.eulerAngles.x + 42,
                                                ourCameraPlane.transform.eulerAngles.y,
                                                ourCameraPlane.transform.eulerAngles.z);

        ourCameraPlane.transform.position = new Vector3(ourCameraPlane.transform.position.x,
                                                ourCameraPlane.transform.position.y - 1.1f,
                                                ourCameraPlane.transform.position.z);

        originalCamPosition = Camera.main.transform.position;
         originalCamRotation = Camera.main.transform.rotation;


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
        if (Input.touchCount == 2 && selected_object == null)
        {

            Touch[] all_touches = Input.touches;
            Touch first_touch = all_touches[0];
            Touch second_touch = all_touches[1];

            if (first_touch.phase == TouchPhase.Began || second_touch.phase == TouchPhase.Began)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                startDistance = Vector3.Distance(touch0, touch1);
                startAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);
                foreach (ITouchController manager in managers)
                    (manager as ITouchController).startRotateScale();
                
            }

            else if (first_touch.phase == TouchPhase.Began && second_touch.phase == TouchPhase.Moved)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                float endDistance = Vector2.Distance(touch0, touch1);
                float diff = endDistance / startDistance;
                float latestAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);
                float actualAngle = latestAngle - startAngle;
                float rotationDegrees = Mathf.Rad2Deg * actualAngle;
                foreach (ITouchController manager in managers)
                    (manager as ITouchController).rotateScale(rotationDegrees, endDistance, diff);


            }
            else 
            {
                Touch touch = Input.touches[0];
                Touch touchOne = Input.touches[1];
                float initialtouchpos = touch.position.y;
                float prevDistance = Vector2.Distance(touch.position - touch.deltaPosition,
                                                      touchOne.position - touchOne.deltaPosition);
                float distance = Vector2.Distance(touch.position, touchOne.position);

                moveCam(distance - prevDistance);
                verticalDrag(initialtouchpos);

            }

        }



        else if (Input.touchCount == 2 && selected_object != null)
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                startDistance = Vector3.Distance(touch0, touch1);
                startAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);
                foreach (ITouchController manager in managers)
                    (manager as ITouchController).startRotateScale();
            }

            else if (Input.touches[1].phase == TouchPhase.Moved)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                float endDistance = Vector2.Distance(touch0, touch1);
                float diff = endDistance / startDistance;
                float latestAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);
                float actualAngle =  startAngle - latestAngle;
                float rotationDegrees = Mathf.Rad2Deg * actualAngle;
                foreach (ITouchController manager in managers)
                    (manager as ITouchController).rotateScale(rotationDegrees, endDistance, diff);


            }

            else if (Input.touches[0].phase == TouchPhase.Moved && Input.touches[1].phase == TouchPhase.Moved)
            {

                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                has_moved = true;


                if (has_moved == true)
                {
                    foreach (ITouchController manager in managers)
                        (manager as ITouchController).drag(Input.touches[0].position);

                }


            }
        }


    }


    public void ResetCameraPosition()
    {
        Camera.main.transform.position = originalCamPosition;
        Camera.main.transform.rotation = originalCamRotation;
    }


}
