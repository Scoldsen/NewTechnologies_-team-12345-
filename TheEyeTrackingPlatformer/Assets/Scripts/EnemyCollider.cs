using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public GameObject enemy;

    private EnemyContoroller enemyContoroller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light"))
        {
            enemyContoroller = enemy.GetComponent<EnemyContoroller>();
            enemyContoroller.scared = true;
        }
    }
}
