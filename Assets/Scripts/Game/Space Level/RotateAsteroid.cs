using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAsteroid : MonoBehaviour
{
    int rotationDirection;
    private void Start()
    {
        rotationDirection = Random.Range(0, 2);
    }
    private void Update()
	{
        if(rotationDirection == 0)
		    transform.Rotate(0, 0, 20 * Time.deltaTime);
        else
            transform.Rotate(0, 0, -20 * Time.deltaTime);
    }
}
