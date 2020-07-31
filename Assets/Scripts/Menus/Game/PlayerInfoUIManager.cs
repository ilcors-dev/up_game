using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
using UB.Simple2dWeatherEffects.Standard;
public class PlayerInfoUIManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI coinsText;
    private TextMeshProUGUI bestScoreText;
    public Player playerInfo;
    //private bool bestScored = false;
    public GameObject ScoreUpdateValueAnimationPrefab;
    public SpawnRows spawner;
    public D2FogsPE stormHandler;

    const int HILLS = 0;
    const int DESERT = 1;
    const int JUNGLE = 2;
    const int SPACE = 3;

    int scoreHolder = 0, count = 1;
    AudioManager audioManager;
    private void Start()
    {
        TextMeshProUGUI[] temp = FindObjectsOfType<TextMeshProUGUI>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.name == "Main Score")
                scoreText = temp[i];
            if (temp[i].gameObject.name == "Coin Counter")
                coinsText = temp[i];
            if (temp[i].gameObject.name == "Best Score")
                bestScoreText = temp[i];
        }
        if (Player.activeLevelID == DESERT)
        {
            stormHandler.HorizontalSpeed = 0.2f;
            stormHandler.Density = 0.2f;
        }
        else
        if (Player.activeLevelID == JUNGLE)
        {
            stormHandler.VerticalSpeed = 0.25f;
            stormHandler.HorizontalSpeed = 0.2f;
            stormHandler.Density = 0.10f;
        }
        playerInfo = FindObjectOfType<Player>();
        BestScoreUpdater();
        scoreHolder = 0;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ScoreUpdater()
    {
        int score = Player.score;
        if (score != 80)//it would be too difficult, so after 80 the difficulty remains the same
        {
            if (score > scoreHolder + 11)
            {
                Debug.Log(score + "score handler: "+ scoreHolder);
                audioManager.ChangeSpeed("GameMusic", 0.02f);
                SpawnRows.spawnCoinDelay -= 0.05f;
                SpawnRows.fallingSpeed += 0.2f;
                ScrollBackground.speed += 0.1f;
                playerInfo._speed += 0.05f;
                LevelGenerationSettings(Player.activeLevelID);
                //playerInfo._jumpForce += 0.05f;
                scoreHolder += 10;
                count++;
                //Debug.LogWarning("Falling Speed : " + SpawnRows.fallingSpeed);
                //Debug.LogWarning("Difficulty : " + SpawnRows.spawnDifficultyDistance);
            }
        }

        Player.score++;
        scoreText.text = Player.score.ToString();
        if (Player.score > Player.bestScore)
        {
            //bestScored = true;
            bestScoreText.text = "BEST:\n" + Player.score;
            //scoreText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            StartCoroutine(RainbowScoreText());
        }
    }

    private void LevelGenerationSettings(int activeLevelID)
    {
        switch (activeLevelID)
        {
            default:
                SpawnRows.spawnDifficultyDistance -= 0.1f;
                break;
            case DESERT:
                float refVelocity = 0.0f;
                float targetDensity = stormHandler.Density + 800f;
                stormHandler.Density = Mathf.SmoothDamp(stormHandler.Density, targetDensity, ref refVelocity, 3f);
                //stormHandler.Density = d;
                Debug.Log("Density: " + stormHandler.Density);
                stormHandler.HorizontalSpeed += 0.05f;
                SpawnRows.spawnDifficultyDistance -= 0.05f;
                break;
            case JUNGLE:
                float refVelocity_ = 0.0f;
                float targetDensity_ = stormHandler.Density + 800f;
                stormHandler.Density = Mathf.SmoothDamp(stormHandler.Density, targetDensity_, ref refVelocity_, 3f);
                //stormHandler.Density = d;
                Debug.Log("Density: " + stormHandler.Density);
                stormHandler.VerticalSpeed += 0.10f;
                stormHandler.HorizontalSpeed += 0.05f;
                SpawnRows.spawnDifficultyDistance -= 0.05f;
                break;
            case SPACE:
                if (count % 2 == 0)
                    SpawnRows.meteoriteWaveCount++;
                //Debug.LogWarning("Meteorites per wave : " + SpawnRows.meteoriteWaveCount);
                break;
        }
    }

    private IEnumerator RainbowScoreText()
    {
        while (true)
        {
            Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            bestScoreText.CrossFadeColor(c, 0.7f, true, false);
            scoreText.CrossFadeColor(c, 0.7f, true, false);//TOFIX!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            yield return new WaitForSeconds(0.7f);
            Color c_1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            bestScoreText.CrossFadeColor(c_1, 0.7f, true, false);
            scoreText.CrossFadeColor(c_1, 0.7f, true, false);//TOFIX!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    public void CoinsUpdater(int moneyValue)
    {
        playerInfo.coins += moneyValue;
        coinsText.text = playerInfo.coins.ToString();
    }

    public void BestScoreUpdater()
    {
        bestScoreText.text = "BEST: \n" + Player.bestScore.ToString();
    }

    public void ShowTextUpdaterAnimation(Vector3 coinPos, int coinValue)
    {
        GameObject scoreValue = Instantiate(ScoreUpdateValueAnimationPrefab, transform);
        scoreValue.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(coinPos);
        scoreValue.GetComponent<TextMeshProUGUI>().text = "+" + coinValue.ToString();
        //Debug.Log(scoreValue.GetComponent<RectTransform>().position.y);
        scoreValue.GetComponent<RectTransform>().DOAnchorPosY(scoreValue.GetComponent<RectTransform>().localPosition.y + 50f, 0.75f);
        scoreValue.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 0.75f, true);
        Destroy(scoreValue, 1f);
    }
    //t = time
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
