﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.LWRP;

public class lightBehavior : MonoBehaviour
{
    [SerializeField]
    public bool keyboardActivated;
    CameraShake camShake;
    Light2D m_Light2D = null;
    public GameObject boss;
    private GazePoint _lastGazePoint = GazePoint.Invalid;
    private bool _hasHistoricPoint;
    private Vector3 _historicPoint;
    //distance from screen to visualization point in the world
    public float VisualizationDistance = 10f;
    //responsiveness filter, lower - more responsive, max is 1
    public float FilterSmoothingFactor = 0.15f;
    public float speed = 0.1f;
    gameManager GameManager;
    public bool blinking = false; // boolean to prevent death when light is simply blinking
    public float maxDistance = 5;
    public float minDistance = 1;
    public float shakeDistance = 0;
    public bool lightOn = true;
    public float distance;
    private IEnumerator blinkCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("_gm").GetComponent<gameManager>();
        m_Light2D = GetComponent<Light2D>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //
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

        dimLight();

        
    }

  
   
    public float checkForDeath()
    {
        if (m_Light2D.enabled && !blinking) {
            return m_Light2D.intensity;
                } else
        {
            return 1;
        }
    }
    void checkForEyesClosing()
    {
        UserPresence user = TobiiAPI.GetUserPresence();
        if (m_Light2D != null)
        {
            if (user.IsUserPresent())
            {
                m_Light2D.enabled = true;
                lightOn = true;
            }
            else
            {
                m_Light2D.enabled = false;
                lightOn = false;
            }
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
    //dims light when boss gets closer
    public void dimLight()
    {
        
        distance = Vector2.Distance(transform.position, boss.transform.position);
        int mode =  0;
        if(distance < minDistance)
        {
            mode = 2;
        } else
        {
            if (distance < maxDistance)
            {
                mode = 1;
            }
            else
            {
                mode = 0;
            }
        }
        
        switch (mode)
        {
            case 0:
                m_Light2D.intensity = 1;
                
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
                break;
            case 1:
                blinking = true;
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = Blinking();
                   
                    StartCoroutine(blinkCoroutine);
                }
                break;
            case 2:
                
                m_Light2D.intensity = 0;
                blinking = false;
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
                break;
        }

        if(distance <= shakeDistance && lightOn)
        {
            CauseCameraShake(0.05f, (0.2f*(Mathf.Abs(1 - shakeDistance/distance))));
        } 
        //change shake based on distance
        
    }

    private void CauseCameraShake(float duration, float strength)
    {
       
        camShake.shakeDuration = duration;
        camShake.shakeAmount = strength;
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

    private IEnumerator Blinking()
    {
        while (true)
        {
            
            m_Light2D.intensity = 0;
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            m_Light2D.intensity = distance/maxDistance;
            //m_Light2D.pointLightOuterRadius =   distance / maxDistance;
            //m_Light2D.pointLightInnerRadius = distance / maxDistance;
            float waitTime = (distance - minDistance) / 5.0f;
            yield return new WaitForSeconds(Random.Range(0.1f, 1) * waitTime);
        }
    }

    private IEnumerator theEnd()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);


    }

}
