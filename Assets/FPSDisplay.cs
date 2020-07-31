using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	float frameCount = 0f;
	float dt = 0.0f;
	float fps = 0.0f;
	float updateRate = 4.0f;  // 4 updates per sec.
	private TextMeshProUGUI fpsUI;
	private void Start()
	{
		fpsUI = GetComponent<TextMeshProUGUI>();
	}
	void Update()
	{
		frameCount++;
		dt += Time.deltaTime;
		if (dt > 1.0f / updateRate)
		{
			fps = frameCount / dt;
			frameCount = 0f;
			dt -= 1.0f / updateRate;
		}
		fpsUI.text = ((int)fps).ToString();
	}
}
