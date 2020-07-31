using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseScript : MonoBehaviour
{
	public bool isPaused = false;

	[SerializeField]
	public GameObject pauseMenu;
	//private GameObject panel;

	//if pauseMenu is shown do not show another
	public bool isShown = false;
	public LevelChanger levelChanger;
	public void ShowPauseMenu()
	{
		if (!isShown && !Player.isDead)
		{
			isShown = true;
			//panel = Instantiate(pauseMenu);
			//pauseMenu.transform.SetParent(GameObject.Find("Pause Menu UI").transform);
			pauseMenu.SetActive(true);
			Button mainMenuButton = pauseMenu.GetComponentsInChildren<Button>()[0];
			mainMenuButton.onClick.AddListener(() => MainMenu());
			//Button resumeButton = pauseMenu.GetComponentsInChildren<Button>()[1];
			//resumeButton.onClick.AddListener(() => ResumeGame());
            FindObjectOfType<AudioManager>().Pause("GameMusic");
            PauseGame();
		}
		else if (isShown && !Player.isDead) {
            FindObjectOfType<AudioManager>().Resume("GameMusic");
            ResumeGame();//you can either click on the pause icon again to resume the game
        }
	}

	public void PauseGame()
	{
		if (!isPaused)
		{
			Time.timeScale = 0;
			isPaused = true;
		}
	}

	public void MainMenu()
	{
		if (isPaused)
		{
			ResumeGame();
			levelChanger.FadeToLevel(0);
		}
	}
	public void ResumeGame()
	{
		if (isPaused)
		{
			HidePauseMenu();
			Time.timeScale = 1;
			isPaused = false;
		}
	}
	public void HidePauseMenu()
	{
		//Destroy(panel);
		pauseMenu.SetActive(false);
		isShown = false;
	}
}
