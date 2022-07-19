using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{

    public PostProcessVolume PPV;
    
    [HideInInspector]
    public Vignette _vignette;

    public static GameObject observedObject = null;
    private float moveVertical = 0f, moveHorizontal = 0f;
    private int internCoeffX = 1500;
    private int internCoeffY = 100;
    private float minY = -70;
    private float maxY = 70;

    // Start is called before the first frame update
    void Start()
    {
        moveHorizontal = 0f;
        moveVertical = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PPV.profile.TryGetSettings(out _vignette);
    }

    // Update is called once per frame
    void Update()
    {
        //get object in camera sight
        observedObject = RayCastManager.GetRayCastedGameObjectFromCamera();

        if(observedObject != null)
        {
            //Debug.Log("Observing: " + observedObject.name);
        }
        else
        {
            //Debug.Log("Looking to Infinite....");
        }

        if (!GameManager.isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (GameManager.canPlayer.controlCamera)
            {
                moveHorizontal += Input.GetAxis("Mouse X") * internCoeffX * Time.deltaTime;
                moveVertical += Input.GetAxis("Mouse Y") * internCoeffY * Time.deltaTime;
                moveVertical = Mathf.Clamp(moveVertical, minY, maxY);

                transform.localEulerAngles = new Vector3(-moveVertical, moveHorizontal, 0f);
            }
        }
    }
}
