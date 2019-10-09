using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablePlank : MonoBehaviour
{
    public GameObject brokenPlank;

    public List<GameObject> gameobjects;

    [HideInInspector]
    public int listSize, listSizeOld = 0;

    public float currentWeight = 0;
    public float maxWeight = 1000;

    private void Start()
    {
        currentWeight = maxWeight == 0 ? 1 : 0;
    }

    private void Update()
    {
        listSize = gameobjects.Count;

        if (listSize != listSizeOld)
        {
            currentWeight = maxWeight == 0 ? 1 : 0;

            foreach (GameObject go in gameobjects)
            {
                currentWeight += go.GetComponent<Rigidbody2D>().mass;
            }

            listSizeOld = listSize;
        }


        if (currentWeight > maxWeight && maxWeight != 0)
        {
            Instantiate(brokenPlank, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
