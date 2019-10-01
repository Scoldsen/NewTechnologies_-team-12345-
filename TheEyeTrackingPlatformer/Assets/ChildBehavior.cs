using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehavior : MonoBehaviour
{
   public  bool inLightRange = true;
    public bool inTheLight = false;
    public float difference;
    public float lightFollowDistance;
    public float maxLightDistance;
    public float movementSpeedFactor;
    public GameObject theLight;
    Vector2 lightPosition;
    Vector2 myPosition;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        checkIfInLight();
        myPosition = transform.position;
        lightPosition = theLight.transform.position;
       
         moveTowardsLight();
       
           // Debug.Log("Not in light, losing health");
        
    }

    void checkIfInLight()
    {
        if(Vector2.Distance(myPosition,lightPosition) <= maxLightDistance)
        {
            difference = Vector2.Distance(myPosition, lightPosition);
            inLightRange = true;
        } else
        {
            inLightRange = false;
        }

        if (Vector2.Distance(myPosition, lightPosition) <= lightFollowDistance)
        {

            inTheLight = true;
        }
        else
        {
            inTheLight = false;
        }
    }

    void moveTowardsLight()
    {
        if(!inTheLight && inLightRange)
        {
           
              
            Rigidbody2D mybody = GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(lightPosition.x - myPosition.x,0);
            direction.Normalize();
            mybody.MovePosition(Vector3.Lerp(myPosition, myPosition+direction*2, movementSpeedFactor));

//            mybody.MovePosition(Vector3.Lerp(myPosition, new Vector2(lightPosition.x, myPosition.y), movementSpeedFactor));
               
            
        }
    }
}
