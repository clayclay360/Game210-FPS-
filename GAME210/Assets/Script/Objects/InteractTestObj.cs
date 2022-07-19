using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTestObj : MonoBehaviour, IInterable
{
    public string objName;
    private bool isInteractable = true;
    private bool canInteract = false;
    public void Interact()
    {
        Debug.Log("Interacted with " + objName);
        isInteractable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInteractable && canInteract)
        {
            Interact();
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            if(CameraController.observedObject != null 
                && CameraController.observedObject.name == name)
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }
}
