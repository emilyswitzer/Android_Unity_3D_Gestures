using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    GameObject sphere;
    GameObject cube;
    GameObject capsule;
    GameObject touchManager;
    GameObject plane;

    // Start is called before the first frame update
    void Start()
    {

        Camera.main.transform.position = new Vector3(-0.2f, 3.71f, -9.66f);


        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 0, 0);
        plane.transform.localScale = new Vector3(10, 1, 10);
        plane.GetComponent<Renderer>().material.color = Color.magenta;
        plane.layer = LayerMask.NameToLayer("Plane");

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.AddComponent<SphereController>();
        sphere.transform.position = new Vector3(0, 1.5f, 0);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<CubeController>();
        cube.transform.position = new Vector3(0, 0.5f, 0);

        capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.AddComponent<CapsuleController>();
        capsule.transform.position = new Vector3(2, 1, 0);

        
        touchManager  = new GameObject("TouchManager");
        touchManager.AddComponent<TouchManager>();
        touchManager.transform.position = new Vector3();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
