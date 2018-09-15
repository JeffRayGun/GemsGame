using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------
// The "back and forth" search light will move forward for a 
// given distance, then turn around at a set speed and move back
// to its starting point, where it repeats the cycle.
// ------------------------------------------------------------------

public class SearchMove : Searchlight
{
    public float speed;
    public float rotateSpeed;
    public float distanceToWalk;
    public bool rotatesClockwise = true;

    private float rotationDirection;
    private float distanceTraveled = 0;
    private float currentAngle = 0;
    private float startingAngle;

    // Use this for initialization
    void Start ()
    {
        if (rotatesClockwise)
            rotationDirection = 1;
        else
            rotationDirection = -1;
        startingAngle = transform.eulerAngles.z;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!catchingPlayer)
        { 
	        if (distanceTraveled > distanceToWalk)
            {
                // Rotate 180 degrees, then 
                StartCoroutine("Rotate");
            }
            else
            {
                transform.Translate(new Vector3(0,-1 * speed * Time.deltaTime,0));
                distanceTraveled += speed * Time.deltaTime;
            }
        }
    }

    IEnumerator Rotate()
    {
        if (transform.eulerAngles.z < 180 + startingAngle && startingAngle <= transform.eulerAngles.z)
        {
            while (transform.eulerAngles.z < 180 + startingAngle )
            {
                // If player is caught, we want to stop this rotation routine:
                if (catchingPlayer == true) break; 

                transform.Rotate(0, 0, rotateSpeed * rotationDirection * Time.deltaTime);
                if (transform.eulerAngles.z > 180 + startingAngle)
                {
                    transform.eulerAngles = new Vector3(0, 0, 180 + startingAngle);
                }
                yield return null;
            }
        }
        else // transform is equal to 180
        {
            while (transform.eulerAngles.z >= 180 + startingAngle || startingAngle > transform.eulerAngles.z)
            {
                // If player is caught, we want to stop this rotation routine:
                if (catchingPlayer == true) break;

                transform.Rotate(0, 0, rotateSpeed * rotationDirection * Time.deltaTime);
                if (transform.eulerAngles.z > startingAngle && transform.eulerAngles.z < 180 + startingAngle)
                {
                    transform.eulerAngles = new Vector3(0, 0, startingAngle);
                }
                yield return null;
            }
        }
        distanceTraveled = 0;
        
    }

}
