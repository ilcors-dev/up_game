using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGScroll : MonoBehaviour//NO LONGER USED
{
	[SerializeField]
	private float _scrollSpeed = 0.1f;

	private MeshRenderer _meshRenderer;

	private Vector2 _savedOffset;

	// Start is called before the first frame update
	void Start()
	{
		_meshRenderer = GetComponent<MeshRenderer>();

		_savedOffset = _meshRenderer.sharedMaterial.GetTextureOffset("_MainTex");
	}

	// Update is called once per frame
	void Update()
	{
		float y = Time.time * _scrollSpeed;
		Vector2 offset = new Vector2(0, y);
		_meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
	private void OnDisable()
	{
		_meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", _savedOffset);
	}
}
