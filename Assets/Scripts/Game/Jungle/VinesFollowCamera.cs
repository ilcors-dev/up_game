using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesFollowCamera : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Update is called once per frame
    void LateUpdate()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector2(transform.position.x, screenBounds.y/2);
    }
}
