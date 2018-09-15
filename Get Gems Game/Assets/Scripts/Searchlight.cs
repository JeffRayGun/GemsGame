using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchlight : MonoBehaviour
{
    public float catchRotationSpeed = 40;
    protected bool catchingPlayer = false;

    public virtual void CatchPlayer(Vector3 playerPos)
    {
        catchingPlayer = true;
        StartCoroutine("FacePlayer", playerPos);
    }

    IEnumerator FacePlayer(Vector3 playerPos)
    {
        // Code for rotation collected from robertbu on StackExchange
        // https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        var dir = transform.position - playerPos; // Get direction vector to look at
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; // Get the angle to rotate to:

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // While the angle is not proper, we
        while (Quaternion.Angle(targetRotation, transform.rotation) > 1)
        {
            // Interpolation of the angle to rotate during this current frame:
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * catchRotationSpeed);            
            yield return null;
        }
    }
}
