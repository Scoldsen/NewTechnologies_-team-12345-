using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoroller : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public BoxCollider2D boxCollider;
    public bool foundPlayer;

    private Rigidbody2D rb;
    private int dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = -1;
        foundPlayer = false;
    }

    void FixedUpdate()
    {
        if (!foundPlayer)
        {
            SetRandomDirection();
        }
        else 
        {
            SetTargetDirction();
        }
            
        transform.localScale = new Vector3(dir, 1, 1);
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
            dir = 1;

        }
        else if (xVector < 0)
        {
            dir = -1;
        }


    }
}
