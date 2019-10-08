using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class lightBehavior : MonoBehaviour
{
    [SerializeField]
    public bool keyboardActivated;
    UnityEngine.Experimental.Rendering.LWRP.Light2D m_Light2D = null;
    private GazePoint _lastGazePoint = GazePoint.Invalid;
    private bool _hasHistoricPoint;
    private Vector3 _historicPoint;
    //distance from screen to visualization point in the world
    public float VisualizationDistance = 10f;
    //responsiveness filter, lower - more responsive, max is 1
    public float FilterSmoothingFactor = 0.15f;
    public float speed = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyboardActivated)
        {
            checkForEyesClosing();
            GazePoint gazePoint = TobiiAPI.GetGazePoint();

            if (gazePoint.IsRecent()
                && gazePoint.Timestamp > (_lastGazePoint.Timestamp + float.Epsilon))
            {

                UpdateGazeBubblePosition(gazePoint);


                _lastGazePoint = gazePoint;
            }
        } else
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 direction = new Vector2(horizontal*0.1f, vertical*0.1f);
            transform.Translate(direction);
        }

        
    }

    void checkForEyesClosing()
    {
        UserPresence user = TobiiAPI.GetUserPresence();
        if(user.IsUserPresent())
        {
            m_Light2D.enabled = true;
        } else
        {
            m_Light2D.enabled = false;
        }
    }
    private void UpdateGazeBubblePosition(GazePoint gazePoint)
    {
        Vector3 gazePointInWorld = ProjectToPlaneInWorld(gazePoint);
        transform.position = Smoothify(gazePointInWorld);
    }

   
    private Vector3 ProjectToPlaneInWorld(GazePoint gazePoint)
	{
		Vector3 gazeOnScreen = gazePoint.Screen;
		gazeOnScreen += (transform.forward * VisualizationDistance);
		return Camera.main.ScreenToWorldPoint(gazeOnScreen);
	}

    private Vector3 Smoothify(Vector3 point)
    {
        if (!_hasHistoricPoint)
        {
            _historicPoint = point;
            _hasHistoricPoint = true;
        }

        var smoothedPoint = new Vector3(
            point.x * (1.0f - FilterSmoothingFactor) + _historicPoint.x * FilterSmoothingFactor,
            point.y * (1.0f - FilterSmoothingFactor) + _historicPoint.y * FilterSmoothingFactor,
            point.z * (1.0f - FilterSmoothingFactor) + _historicPoint.z * FilterSmoothingFactor);

        _historicPoint = smoothedPoint;

        return smoothedPoint;
    }
}
