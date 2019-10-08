using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    public GameObject enemyObject;

    private EnemyContoroller enemy;

    private void Start()
    {
        enemy = enemyObject.GetComponent<EnemyContoroller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.foundPlayer = true;
            Debug.Log("find Player");
        }
    }
}
