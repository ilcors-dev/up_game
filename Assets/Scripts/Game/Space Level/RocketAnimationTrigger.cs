using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimationTrigger : MonoBehaviour
{
	public Animator anim;
	private Vector3 cameraPos;
	private bool once = false;
	private void Awake()
	{
		cameraPos = Camera.main.transform.position;
	}
	private void LateUpdate()
	{
		if (!once)
		{
			cameraPos = Camera.main.transform.position;
			if (transform.position.y <= cameraPos.y + Camera.main.orthographicSize / 2)
			{
				once = true;
				anim.SetBool("start", true);
			}
		}
	}
}
