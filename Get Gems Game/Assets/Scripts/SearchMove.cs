﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------
// The "back and forth" search light will move forward for a 
// given distance, then turn around at a set speed and move back
// to its starting point, where it repeats the cycle.
// ------------------------------------------------------------------

public class SearchMove : MonoBehaviour {

    public float speed;
    public float rotateSpeed;
    public float distanceToWalk;
    public bool rotatesClockwise = true;

    private float rotationDirection;
    private float distanceTraveled = 0;
    private float currentAngle = 0;

    // Use this for initialization
    void Start ()
    {
        if (rotatesClockwise)
            rotationDirection = 1;
        else
            rotationDirection = -1;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
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

    IEnumerator Rotate()
    {
        if (transform.eulerAngles.z < 180)
        {
            while (transform.eulerAngles.z < 180)
            {
                transform.Rotate(0, 0, rotateSpeed * rotationDirection * Time.deltaTime);
                if (transform.eulerAngles.z > 180)
                {
                    transform.eulerAngles = new Vector3(0, 0, 180);
                }
                yield return null;
            }
        }
        else // transform is equal to 180
        {
            while (transform.eulerAngles.z >= 180)
            {
                transform.Rotate(0, 0, rotateSpeed * rotationDirection * Time.deltaTime);
                if (transform.eulerAngles.z < 180)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                yield return null;
            }
        }
        distanceTraveled = 0;
        
    }
}
