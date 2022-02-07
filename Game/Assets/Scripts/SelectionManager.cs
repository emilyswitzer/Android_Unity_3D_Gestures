using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    IInteractable prevSelection;
    IInteractable currentSelection;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit_info;
            if (Physics.Raycast(ourRay, out hit_info))
            {
                IInteractable selectedObject = hit_info.transform.GetComponent<IInteractable>();
                if (selectedObject != null)
                {

                }
            }
        }
    }


}
