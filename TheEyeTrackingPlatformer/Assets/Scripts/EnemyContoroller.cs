using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoroller : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public bool foundPlayer;
    public bool scared;

    private Rigidbody2D rb;
    private float dir;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = -1;
        foundPlayer = false;
        currentTime = 0.0f;
    }

    void FixedUpdate()
    {
        if (scared)
        {
            currentTime += Time.deltaTime;
            if(target.transform.position.x - transform.position.x >0)
            {
                Debug.Log("Running Left");
                dir = -0.35f;
            } else
            {
                Debug.Log("Running Right");
                dir = 0.35f;
            }
            
            if (currentTime > 1.0f)
            {
                currentTime = 0.0f;
                scared = false;
            }
        }
        else
        {
            if (!foundPlayer)
            {
                SetRandomDirection();
            }
            else
            {
                SetTargetDirction();
            }
        }


        transform.localScale = new Vector3(-dir, 0.35f, 0.35f);
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    void SetRandomDirection()
    {
        int random = Random.Range(0, 50);
        if (random == 0)
            dir = -dir;
    }

    void SetTargetDirction()
    {
        float xVector = target.transform.position.x - transform.position.x;

        if (xVector > 0)
        {
            dir = 0.35f;

        }
        else if (xVector < 0)
        {
            dir = -0.35f;
        }


    }
}
