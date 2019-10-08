using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightScale : MonoBehaviour
{
    public GameObject leftPlatform, rightPlatform;
    public GameObject leftPulley, rightPulley;

    LineRenderer LR;

    public float M_1, M_2;
    public float distFromTop = 0.4f;

    float defBottomLeft, defBottomRight;

    public float accel;
    public float distanceLeft, distanceRight;

    float speed = 0;

    private void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.SetPosition(1, leftPulley.transform.position);
        LR.SetPosition(2, rightPulley.transform.position);

        distanceLeft = Vector2.Distance(leftPlatform.transform.position, leftPulley.transform.position);
        distanceRight = Vector2.Distance(rightPlatform.transform.position, rightPulley.transform.position);

        defBottomLeft = leftPulley.transform.position.y - distanceLeft - distanceRight + distFromTop;
        defBottomRight = rightPulley.transform.position.y - distanceLeft - distanceRight + distFromTop;
    }

    private void FixedUpdate()
    {
        accel = -Physics2D.gravity.y * (M_2 - M_1) / (M_1 + M_2);

        float leftYPos = Mathf.Min(Mathf.Max(leftPlatform.transform.position.y + speed, defBottomLeft), leftPulley.transform.position.y - distFromTop);
        float rightYPos = Mathf.Min(Mathf.Max(rightPlatform.transform.position.y - speed, defBottomRight), rightPulley.transform.position.y - distFromTop);

        Vector3 leftNew = new Vector3(leftPlatform.transform.position.x, leftYPos, leftPlatform.transform.position.z);
        Vector3 rightNew = new Vector3(rightPlatform.transform.position.x, rightYPos, rightPlatform.transform.position.z);

        if ((accel < 0 && rightPlatform.transform.position.y >= rightPulley.transform.position.y - distFromTop))
        {
            speed = 0;
        }
        else if ((accel > 0 && leftPlatform.transform.position.y >= leftPulley.transform.position.y - distFromTop))
        {
            speed = 0;
        }
        else
        {
            speed += accel * Time.fixedUnscaledDeltaTime;

            leftPlatform.transform.position = leftNew;
            rightPlatform.transform.position = rightNew;
        }

        LR.SetPosition(0, leftPlatform.transform.position);
        LR.SetPosition(3, rightPlatform.transform.position);
    }
}
