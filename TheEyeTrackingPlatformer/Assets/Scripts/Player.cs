using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float walkSpeed = 6;
    public float runSpeed = 10;

    float gravity;
    float jumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below || (!Input.GetKey(KeyCode.Space) && velocity.y > 0))
        {
            print("Collides!");
            velocity.y = 0;
        }
        
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space)/*Input.GetAxis("Vertical") > 0*/ && controller.collisions.below)
        {
            FindObjectOfType<AudioManager>().playSound("bigjump");
            velocity.y = jumpVelocity;           
        }
        float targetVelocityX = input.x * walkSpeed;

        if (Input.GetKey(KeyCode.LeftControl)) {
            targetVelocityX = input.x * runSpeed;
        }

        
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /*
        if (transform.position.y < 0)
        {
            controller.Respawn();
        }*/
    }
}