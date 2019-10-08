using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablePlank : MonoBehaviour
{
    public float currentWeight = 0;

    public float maxWeight = 1000;
    public GameObject brokenPlank;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            currentWeight += rb.mass;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            currentWeight -= rb.mass;
        }
    }

    private void FixedUpdate()
    {
        if (currentWeight > maxWeight)
        {
            Instantiate(brokenPlank, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
