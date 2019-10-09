using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public List<GameObject> gameobjects;
    public int listSize, listSizeOld = 0;

    public breakablePlank plank;
    public breakablePlank lastPlank;

    public int collidesWithNshapes;
    public bool collidesWithPlank = false;

    public Rigidbody2D rb;
    public float mass = 0;

    public LineRenderer lineRenderer;
    public PolygonCollider2D polygon;
    public int numberOfPoints;
    List<Vector2> points;
    gameManager gm;



    public void UpdateLine(Vector2 mousePos)
    {
        if(points!=null)
        {
            numberOfPoints = points.Count;
        }

        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) > .1f)
            SetPoint(mousePos);
    }

    public void CloseLine()
    {
        if (polygon != null)
        {
            Vector2 vector = new Vector2(points.First().x, points.First().y);
            SetPoint(vector);

            polygon.SetPath(0, points);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, point);
        }

    }

    private void OnMouseDown()
    {
        gm = GameObject.Find("_gm").GetComponent<gameManager>();
        gm.removeDrawingAmount(gameObject.GetComponent<Rigidbody2D>().mass);
        Destroy(gameObject);
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        Line lineObj = go.GetComponent<Line>();
        breakablePlank plankObj = go.GetComponent<breakablePlank>();

        //botst met lineobj
        if (plankObj != null)
        {
            collidesWithPlank = true;
            plank = plankObj;
        }

        else if (lineObj != null)
        {
            if (lineObj.plank != null) plank = lineObj.plank;
            collidesWithNshapes++;
        }

        if (plank != null && !plank.gameobjects.Contains(gameObject))
        {
            plank.gameobjects.Add(gameObject);
        }

        gm = GameObject.Find("_gm").GetComponent<gameManager>();
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gm.removeDrawingAmount(gameObject.GetComponent<Rigidbody2D>().mass);
            Destroy(gameObject);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        Line lineObj = go.GetComponent<Line>();
        breakablePlank plankObj = go.GetComponent<breakablePlank>();

        if (lineObj != null) collidesWithNshapes--;

        else if (plankObj != null)
        {
            collidesWithPlank = false;
        }

        if (!collidesWithPlank && collidesWithNshapes == 0 && plank != null)
        {
            plank.gameobjects.Remove(gameObject);
            plank = null;
        }
    }

}