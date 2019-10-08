using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{

    public GameObject linePrefab;
    gameManager GameManager;
    public Material spriteMaterial;
    LineRenderer myRenderer;
    Line activeLine;
    private Rigidbody2D rigidbody;
    GameObject LineGameObject;

    private void Start()
    {
        GameManager = GameObject.Find("_gm").GetComponent<gameManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
            rigidbody = lineGO.GetComponent<Rigidbody2D>();
            LineGameObject = lineGO;

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

            if (massOfBody<3)
            {
                Destroy(LineGameObject);
            } else
            {
                GameManager.UpdateDrawingAmount(rigidbody.mass);
            }


        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

    }

}