using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool onGround = false;
    public int health = 3;
    int direction = 0; // 0 is right and 1 is left.
    CameraShake camShake;
    Vector3 pos = Vector3.zero;
    Vector3 posOld = Vector3.zero;

    //public bool walking = false;

    public Animator animator;

    private void Start()
    {
        pos = transform.position;
        posOld = pos;
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

    }

    void Update()
    {
        checkForDeath();
        checkIfInLight();
        checkIfOnGround();
        myPosition = transform.position;
        lightPosition = theLight.transform.position;
       
        moveTowardsLight();

        pos = transform.position;

        if(!onGround)
        {
            animator.SetInteger("state", 2);
        }
    }

    void checkForDeath()
    {   
        if(health<=0)
        {
            IEnumerator endTime = theEnd();
            StartCoroutine(endTime);
            Debug.Log("Game over");
        }
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
            if (onGround)
            {
                animator.SetInteger("state", 0);
            }
        }
        else
        {
            inTheLight = false;
        }
    }

    void checkIfOnGround()
    {

        Vector3 offset = new Vector3(0, 0, 0);

        if (direction == 0)
        {
            offset = new Vector3(-0.2f, -0.8f, 0);
            transform.localScale = new Vector2 (Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } else if (direction == 1)
        {
            offset = new Vector3(0.2f, -0.8f, 0);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, Vector2.down, 1f);

        if (hit.collider != null)
        {
            
                if (hit.distance <= 0.5f)
                {
                    
                    onGround = true;
                }
                else
                {
                    onGround = false;
                }
            
        } else
        {
            onGround = false;
        }
    }
    void moveTowardsLight()
    {
        Rigidbody2D mybody = GetComponent<Rigidbody2D>();

        if (!inTheLight && inLightRange && onGround)
        {
           
              
            
            Vector2 direction = new Vector2(lightPosition.x - myPosition.x,0);
            direction.Normalize();
            mybody.MovePosition(Vector3.Lerp(myPosition , myPosition+direction*2, movementSpeedFactor));

            if(onGround)
            {
                animator.SetInteger("state", 1);
            }
            
            
//            mybody.MovePosition(Vector3.Lerp(myPosition, new Vector2(lightPosition.x, myPosition.y), movementSpeedFactor));
               
            
        }

        if (myPosition.x > lightPosition.x +  0.5f)
        {
            direction = 1;
            
        } else if (myPosition.x + 0.5f < lightPosition.x)
        {
            direction = 0;
        }
        
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            camShake.shakeDuration = 1f;
            camShake.shakeAmount = 0.3f;
            Vector2 direction =  transform.position - collision.transform.position;
            direction.Normalize();
            collision.rigidbody.AddForce(direction * 100);

            
        }
    }

    private IEnumerator theEnd()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        

    }
}
