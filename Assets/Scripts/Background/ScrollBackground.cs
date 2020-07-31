using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
	private Rigidbody2D rigid;
	public static float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        rigid = GetComponent<Rigidbody2D>();
		rigid.velocity = new Vector2(0, -speed);
    }

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		rigid.velocity = new Vector2(0, -speed);
	}

	// Update is called once per frame
	void Update()
    {
		rigid.velocity = new Vector2(0, -speed);
    }
}
