using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpacePlatform : MonoBehaviour
{
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (Player.activeLevelID == 3)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				Destroy(transform.gameObject);
			}
		}
	}
}
