using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    bool bossActive = false;
    public GameObject playerLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkDistanceFromPlayer()
    {
        float distance = Vector2.Distance(playerLight.transform.position, transform.position);
    }
}
