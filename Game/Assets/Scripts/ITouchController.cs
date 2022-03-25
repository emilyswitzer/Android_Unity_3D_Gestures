using UnityEngine;

public interface ITouchController 
{


    void tap(Vector2 position);

    void drag(Vector2 current_position);

    void drag_ended();

    void moveCameraAcross(float position, Touch touch, Touch touch1);


    void rotateScale(float rotationDegrees, float endDistance, float diff);

    void doubleTap(Vector2 position);




}
