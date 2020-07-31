using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class DeathMenu : MonoBehaviour
{
	[SerializeField]
	//public GameObject buttonPrefab;
	public GameObject deathMenu;
	//private Vector2 screen;

	private TextMeshProUGUI lastScoreText;
	private TextMeshProUGUI bestScoreText;
	private TextMeshProUGUI coinsBalanceText;

	public LevelChanger levelChanger;
	private void Start()
	{
		deathMenu.SetActive(false);//it should be set in inspector, but we never know
	//	screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
	}
	/**
	 * On player death a menu with some values(score, best score, money ..) gets shown.
	 * 
	 */
	public void ShowMenu(string bestScore, string score, string coinsBalance)
	{
		//GameObject panel = Instantiate(deathMenu);
		deathMenu.SetActive(true);
		Button button = deathMenu.GetComponentInChildren<Button>();
		lastScoreText = deathMenu.GetComponentsInChildren<TextMeshProUGUI>()[0];
		bestScoreText = deathMenu.GetComponentsInChildren<TextMeshProUGUI>()[1];
		coinsBalanceText = deathMenu.GetComponentsInChildren<TextMeshProUGUI>()[2];
		//button.transform.position = new Vector2(Screen.width / 2, Screen.height / 4);
		lastScoreText.text = "SCORE: " + score;
		bestScoreText.text = "BEST: " + bestScore;
		if (Player.isBestScored == true)
			StartCoroutine(RainbowScoreText());
			//bestScoreText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		coinsBalanceText.text = "COIN BALANCE: " + coinsBalance;
		button.onClick.AddListener(() => RestartButton());
	}

	private IEnumerator RainbowScoreText()
	{
		while (true)
		{
			bestScoreText.CrossFadeColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 0.5f, true, false);
			yield return new WaitForSeconds(0.7f);
			bestScoreText.CrossFadeColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 0.5f, true, false);
		}
	}

	public void RestartButton()
	{
		//restartButton.gameObject.SetActive(false)
		levelChanger.FadeToLevel(1);
		//SceneManager.LoadScene("Game");
	}
	
	public void BackToMainMenu()
	{
		levelChanger.FadeToLevel(0);
	}
}
