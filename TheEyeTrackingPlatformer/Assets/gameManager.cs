using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float drawingUsed = 0;
    public ChildBehavior child;
    public lightBehavior light;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetGame()
    {
        child.gameObject.transform.position = new Vector3(-13.8f, 1.2f, 0f);
        camera.transform.position = new Vector3(-11.83f, 2.41f, -11.01f);
        

    }

    public void resetDrawingAmount()
    {
        drawingUsed = 0;
    }
    public void UpdateDrawingAmount(float mass)
    {
        drawingUsed += mass;
    }

    public void removeDrawingAmount(float mass)
    {
        drawingUsed -= mass;
    }
}
