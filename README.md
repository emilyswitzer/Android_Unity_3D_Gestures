# Android Unity 3D Gestures

## Project Description
This Unity project demonstrates touch gesture controls for manipulating 3D objects and the camera on Android devices. Users can tap, drag, rotate, scale objects, and control the camera using intuitive multi-touch gestures.

## Technologies
- Unity 2020.3 LTS (or your version)
- C#

## Installation / Setup
1. Open the project in Unity.
2. Create a new scene.
3. Create an empty GameObject.
4. Drag the `ObjectGenerator` script onto the empty GameObject.

## How to Use / Controls

### Gestures
- **Tap:** Select an object by tapping it. Tap another object or plane to unselect.
- **Double Tap:** Double tap the plane to reset the camera position.
- **Scale:** Select an object and pinch with two fingers to scale. Without selection, pinch zooms the camera.
- **Drag:** Drag a selected object with one finger. Without selection, drag moves the camera.
- **Two Finger Drag:** Moves the camera when no object is selected.
- **Rotate:** Rotate a selected object by keeping one finger stationary and moving the other. Same gesture rotates camera if no object is selected.

## License
MIT License
