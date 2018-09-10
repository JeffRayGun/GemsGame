using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate : MonoBehaviour {

    public float maxAngleOfRotation; // Set to 0 for continuous rotation
    public float speed;
    public bool startCW = true;
    public float waitTimeAfterRotation;

    private float rotationDirection;
    private float currentAngle;
    private bool isMoving = true;

    void Start()
    {
        if (startCW == true)
        {
            rotationDirection = 1;
        }
        else
        {
            rotationDirection = -1;
        }

        // Setting angle variable to calculate rotation offset:
        currentAngle = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maxAngleOfRotation != 0) // If angled rotation is enabled, we check to see if the rotation has exceeded the maximum
        {
            if (Mathf.Abs(currentAngle) > maxAngleOfRotation && isMoving)
            {
                Debug.Log("Over the angle");
                rotationDirection *= -1;
                
                // If enabled, we wait a given amount of time after this completed rotation cycle.
                if (waitTimeAfterRotation > 0)
                    StartCoroutine("WaitAfterRotate");
            }

            if (isMoving)
            { 
                transform.Rotate(0, 0, speed * rotationDirection);
                currentAngle += speed * rotationDirection;
            }

        }
        else
        {
            transform.Rotate(0, 0, speed * rotationDirection);
        }
    }

    IEnumerator WaitAfterRotate()
    {
        isMoving = false;
        yield return new WaitForSeconds(waitTimeAfterRotation);
        transform.Rotate(0, 0, speed * rotationDirection);
        currentAngle += speed * rotationDirection;
        isMoving = true;
    }
}