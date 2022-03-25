using UnityEngine;

public interface ITouchController 
{


    void tap(Vector2 position);

    void drag(Vector2 current_position);

    void pinch(Vector2 position_1, Vector2 position_2, float relative_distance);

    void drag_ended();

    void moveCameraAcross(float position, Touch touch, Touch touch1);

    void startRotateScale();

    void rotateScale(float rotationDegrees, float endDistance, float diff);

    void doubleTap(Vector2 position);




}
