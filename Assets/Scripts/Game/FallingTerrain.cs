using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTerrain : MonoBehaviour
{
	private Rigidbody2D rb;
	private Vector2 screenBounds;
    float fallingSpeed = -SpawnRows.fallingSpeed;
    private bool ignoreCollisionFlag = false;
    public Collider2D playerCollider;
	// Start is called before the first frame update
	void Start()
	{
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, fallingSpeed);
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Terrain").GetComponent<Collider2D>());
    }

	// Update is called once per frame
	void Update()
	{
        if(gameObject.CompareTag("FallingTile") && Player.isDead && !ignoreCollisionFlag)
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), playerCollider);
        }
        if (transform.position.y < SpawnRows.integerBoundY - 15f)
		{
			Destroy(gameObject);
		}
		//if (transform.position.x < -screenBounds.x - 2 || transform.position.x > screenBounds.x + 2)
		//{
		//	Destroy(gameObject);
		//}
	}
    private void LateUpdate()
    {
        if (fallingSpeed != -SpawnRows.fallingSpeed)
        {
            fallingSpeed = -SpawnRows.fallingSpeed;
            rb.velocity = new Vector2(0, fallingSpeed);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //	if(collision.gameObject.name == "Terrain")
    //	{
    //		Destroy(gameObject, 1f);
    //	}
    //}-0.2500009
}
