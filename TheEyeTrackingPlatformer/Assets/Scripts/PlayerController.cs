using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Animator anim;
    private Rigidbody2D rigidbody;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(SetX(), rigidbody.velocity.y);
    }


    private float SetX()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Walk", true);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("Walk", true);
            xSpeed = -speed;
        }
        else
        {
            anim.SetBool("Walk", false);
            xSpeed = 0.0f;
        }

        return xSpeed;
    }

}
