

using UnityEngine;

public interface ITouchController 
{


    void tap(Vector2 position);

    void drag(Vector2 current_position);

    void pinch(Vector2 position_1, Vector2 position_2, float relative_distance);

    void changeColour(Color color);

    void collision(); //This is to see if it collides with something

}
