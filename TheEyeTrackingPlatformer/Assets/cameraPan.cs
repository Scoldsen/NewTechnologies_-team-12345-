using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class cameraPan : MonoBehaviour
{
    public GameObject camera;
    Vector3 originalPos;
    Camera cam;
    private GazePoint gPoint;
    // Start is called before the first frame update
    void Start()
    {
        cam = camera.GetComponent<Camera>();
        originalPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        KeepItInBounds();

        gPoint = TobiiAPI.GetGazePoint();
        if(gPoint.IsRecent())
            
        {
            if(gPoint.Viewport.x > 0.9f)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + new Vector3(3, 0, 0), 0.03f);
            }
            if (gPoint.Viewport.x < 0.1f)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - new Vector3(3, 0, 0), 0.03f);
            }

            if (gPoint.Viewport.y > 0.9f)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + new Vector3(0, 3, 0), 0.03f);
            }
            if (gPoint.Viewport.y < 0.1f)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - new Vector3(0, 3, 0), 0.03f);
            }
        }


        Vector3 myPositionOnScreen;
        myPositionOnScreen = cam.WorldToViewportPoint(transform.position);
        if(myPositionOnScreen.x > 0.9f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + new Vector3(3, 0, 0), 0.03f);
        }
        if (myPositionOnScreen.x < 0.1f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - new Vector3(3, 0, 0), 0.03f);
        }
        if (myPositionOnScreen.y > 0.9f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + new Vector3(0, 3, 0), 0.03f);
        }
        if (myPositionOnScreen.y < 0.1f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - new Vector3(0, 3, 0), 0.03f);
        }


    }

    public void KeepItInBounds()
    {
        if(camera.transform.position.y > originalPos.y + 1f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, originalPos.y + 1f, camera.transform.position.z);
        }

        if (camera.transform.position.y < originalPos.y - 1f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, originalPos.y - 1f, camera.transform.position.z);
        }
    }
}
