using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float drawingUsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
