using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    public float masTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //masTotal = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(0,100));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            masTotal += collision.gameObject.GetComponent<Rigidbody2D>().mass;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            masTotal -= collision.gameObject.GetComponent<Rigidbody2D>().mass;
        }
    }
}
