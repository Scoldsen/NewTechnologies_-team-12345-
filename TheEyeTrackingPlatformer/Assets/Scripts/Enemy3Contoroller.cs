using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Contoroller : MonoBehaviour
{
    public float stopTime;
    public float attackTime;
    public float shotY;
    public GameObject target;
    public GameObject shotPrefab;

    private Rigidbody2D rb;
    private Rigidbody2D shotRb;
    private int dir;
    private float currentTime;
    private bool shotFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = -1;
        shotFlag = false;
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (stopTime < currentTime && currentTime < stopTime + attackTime)
        {
            if (!shotFlag)
            {
                Debug.Log("hi");
                GameObject shot = Instantiate(shotPrefab);
                shot.transform.position = transform.position;

                float xVector = target.transform.position.x - transform.position.x;

                if (xVector > 0)
                {
                    dir = 1;

                }
                else if (xVector < 0)
                {
                    dir = -1;
                }

                shotRb = shot.GetComponent<Rigidbody2D>();
                float dropTime = 2 * shotY / (shotRb.gravityScale * 10);

                shotFlag = true;
                shotRb.velocity = new Vector2(xVector / dropTime, shotY);
            }

            transform.localScale = new Vector3(dir, 1, 1);
        }
        else if (currentTime > stopTime + attackTime)
        {
            currentTime = 0.0f;
            shotFlag = false;
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
