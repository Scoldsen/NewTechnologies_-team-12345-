using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class cameraPan : MonoBehaviour
{
    public GameObject camera;
    private GazePoint gPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        
    }
}
