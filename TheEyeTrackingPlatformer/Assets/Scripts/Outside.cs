using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.position = new Vector3(5, 10, 0);
        }
    }
}
