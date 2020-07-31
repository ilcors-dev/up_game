using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
	private Vector2 screenBounds;
	Vector3 cameraPos;
	private float cameraYSize;
	bool spawned = false;//to prevent the infinite spawning of backgrounds
	private void Start()
	{
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		//backgroundCollider = GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		cameraPos = Camera.main.transform.position;
		cameraYSize = Camera.main.orthographicSize * 2f;
		if (transform.position.y < -(cameraYSize - cameraPos.y) && !spawned)//if background went over the bottom camera bound
		{
			spawned = true;
			RepeatBackground();
		}
		if (transform.position.y < -screenBounds.y * 3.5f)//3.5f is 0.5 more due to destroying and thread bug
			Destroy(gameObject);
	}

	private void RepeatBackground()
	{
		//Vector2 BGoffset = new Vector2(0, screenBounds.y * 3f);
		GameObject dup = Instantiate(gameObject, new Vector3(0, transform.position.y + cameraYSize * 2f, 0), Quaternion.identity);
		dup.transform.SetParent(transform.parent);
		//transform.position = BGoffset;
	}
}
