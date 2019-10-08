using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{

    public GameObject linePrefab;
    public Material spriteMaterial;
    LineRenderer myRenderer;
    Line activeLine;
    private Rigidbody2D rigidbody;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
            rigidbody = lineGO.GetComponent<Rigidbody2D>();
        }

        if (Input.GetMouseButtonUp(0))
        {

            int massOfBody = activeLine.numberOfPoints;
            
            activeLine.CloseLine();
            myRenderer = activeLine.GetComponent<LineRenderer>();
            activeLine = null;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            myRenderer.material = spriteMaterial; 
            rigidbody.mass = massOfBody*60;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

    }

}