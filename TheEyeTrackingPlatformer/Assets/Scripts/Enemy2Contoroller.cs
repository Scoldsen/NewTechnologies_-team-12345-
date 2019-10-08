using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Contoroller : MonoBehaviour
{
    public float stopTime;
    public float attackTime;
    public float jumpX;
    public float jumpY;
    public GameObject target;

    private Rigidbody2D rb;
    private int dir;
    private float currentTime;
    private bool jumpFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = -1;
        jumpFlag = false;
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (stopTime < currentTime && currentTime < stopTime + attackTime)
        {
            if (!jumpFlag)
            {
                SetTargetDirction();
                jumpFlag = true;
                rb.velocity = new Vector2(dir * jumpX, jumpY);
            }

            transform.localScale = new Vector3(dir, 1, 1);
        }
        else if (currentTime > stopTime + attackTime)
        {
            currentTime = 0.0f;
            jumpFlag = false;
        }

    }

    void SetTargetDirction()
    {
        float xVector = target.transform.position.x - transform.position.x;

        if (xVector > 0)
        {
            dir = 1;

        }
        else if (xVector < 0)
        {
            dir = -1;
        }


    }
}
