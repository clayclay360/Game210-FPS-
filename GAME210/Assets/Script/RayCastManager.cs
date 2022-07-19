using UnityEngine;

public class RayCastManager
{
    public static GameObject GetRayCastedGameObjectFromCamera()
    {
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}