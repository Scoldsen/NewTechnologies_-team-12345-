using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public bool bossActive = false;

    public GameObject playerLight;
    lightBehavior playerLightControls;
    gameManager gm;
    public float drawingLimit;
    float drawingUsed;
    public int direction = 0;
    public bool onGround = false;
    Vector2 myPosition;
    public float movementSpeedFactor;
    Vector3 lightPosition;
    bool countownTimerStarted = false;
    public float killDistance = 0f;
    private IEnumerator waitTimer = null;
    private IEnumerator endTimer;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        playerLightControls = playerLight.GetComponent<lightBehavior>();
        gm = GameObject.Find("_gm").GetComponent<gameManager>();
        
    }

    // Update is called once per frame
    void Update()
    
    {
        lightPosition = playerLight.transform.position;
        myPosition = transform.position;
        drawingUsed = gm.drawingUsed;
        checkDrawingAmount();
        checkIfOnGround();
        
        checkDistanceFromPlayer();
        

    }

    void checkDistanceFromPlayer()
    {
        float distance = Vector2.Distance(playerLight.transform.position, transform.position);

        if(distance<1f)
        {
            waitTimer = waitForPlayerToOpenEyes();
            if (!countownTimerStarted)
            {
                StartCoroutine(waitTimer);
            }
            else { 
                moveAwayFromPlayer();
            }
           
        } else
        {
            if (!countownTimerStarted)
            {
                moveTowardsPlayer();
            } else
            {
                moveAwayFromPlayer();
            }
            
        }

        if (playerLightControls.checkForDeath() <= 0.1)
        {
            if (!dead)
            { 
                //gm.resetGame();
                dead = true;
            }
            
            //play a scream
        }
    }

    private IEnumerator theEnd()
    {
        Time.timeScale = 0;
        
        yield return new WaitForSeconds(3);
        Debug.Log("okay");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);


    }
    public void checkDrawingAmount()
    {
        if(drawingUsed >= drawingLimit)
        {
            bossActive = true;
            
        } else
        {
            bossActive = false;
        }
    }

 


    void moveTowardsPlayer()
    {
        Rigidbody2D mybody = GetComponent<Rigidbody2D>();

            if (bossActive & onGround)
            {

                Vector2 direction = new Vector2(lightPosition.x - myPosition.x, 0);
                direction.Normalize();
                mybody.MovePosition(Vector3.Lerp(myPosition, myPosition + (direction * 1.5f), movementSpeedFactor));


            }

            if (myPosition.x > lightPosition.x + 0.5f)
            {
                direction = 1;

            }
            else if (myPosition.x + 0.5f < lightPosition.x)
            {
                direction = 0;
            }
        


    }

    void moveAwayFromPlayer()
    {

        Rigidbody2D mybody = GetComponent<Rigidbody2D>();

        if (bossActive & onGround)
        {

            Vector2 direction = new Vector2(lightPosition.x - myPosition.x, 0);
            direction.Normalize();
            mybody.MovePosition(Vector3.Lerp(myPosition, myPosition + (direction * -1.5f), movementSpeedFactor));


        }

        if (myPosition.x > lightPosition.x + 0.5f)
        {
            direction = 1;

        }
        else if (myPosition.x + 0.5f < lightPosition.x)
        {
            direction = 0;
        }


    }
    void checkIfOnGround()
    {

        Vector3 offset = new Vector3(0, 0, 0);

        if (direction == 0)
        {
            offset = new Vector3(0f, -0.5f, 0);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (direction == 1)
        {
            offset = new Vector3(0f, -0.5f, 0);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, Vector2.down, 2f);

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

        }
        else
        {
            onGround = false;
        }
    }

    private IEnumerator waitForPlayerToOpenEyes()
    {
        if (!countownTimerStarted)
        {
            
            float randomInterval = Random.RandomRange(5, 8);
            yield return new WaitForSeconds(Random.RandomRange(3, 5));
            countownTimerStarted = true;
            yield return new WaitForSeconds(randomInterval);
            
            countownTimerStarted = false;
        }
    }

}
