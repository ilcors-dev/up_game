using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Cinemachine;
public class Player : MonoBehaviour
{
    private PlayerData data;
    //get reference to rigidbody
    private Rigidbody2D _rigid;
    //public GameObject[] scrollingBg;
    //public GameObject[] scrollingBg;

    #region MOVEMENT
    [SerializeField]
    public float _speed = 2.7f;
    [SerializeField]
    public float _jumpForce = 2.7f;
    #endregion

    public GameObject deathSplashEffect;

    #region SAVES
    public static int score;
    public static int bestScore;
    public static bool isBestScored = false;
    public static List<int> boughtSkinsID = new List<int>();
    public static List<int> boughtLevelsID = new List<int>();
    //private TextMeshProUGUI scoreText;

    public static int activeSkinID;
    public static int activeLevelID;
    #endregion

    #region COIN
    //round coins
    public int coins;
    //total money
    public static int balance;
    #endregion
    //private TextMeshProUGUI bestScoreText;

    public static bool isDead = false;
    private DeathMenu deathMenu;

    private PauseScript pauseHandler;
    public PlayerInfoUIManager playerInfoUIManager;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        isDead = false;
        _speed = 2.7f;
        _jumpForce = 2.7f;

        _rigid = GetComponent<Rigidbody2D>();
        score = 0;
        isBestScored = false;
        activeSkinID = 0;
        activeLevelID = 0;
        coins = 0;
        //boughtLevelID = null;
        
        deathMenu = FindObjectOfType<DeathMenu>();
        pauseHandler = FindObjectOfType<PauseScript>();
        LoadPlayer();
        playerInfoUIManager = FindObjectOfType<PlayerInfoUIManager>();//GameObject.Find("Player Info UI").gameObject;
        
        //BestScoreUpdater();
        if (activeLevelID == 3)//space level
        {
            _rigid.gravityScale = 0.5f;
            _jumpForce = 3.0f;
        }
        //playerInfoUIManager.BestScoreUpdater();
        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = transform;
    }

    void LateUpdate()
    {
        if (!isDead && !pauseHandler.isPaused)
        {
            Movement();
        }
    }

    void Movement()
    {
        //avoid moving player if the user clicks on the menus, if is for windows debug
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    _rigid.velocity = new Vector2(-_speed, _jumpForce);
                }
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    _rigid.velocity = new Vector2(_speed, _jumpForce);
                }
            }
        }
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            //Debug.Log(_touch.position.x);
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    //avoid moving player if the user clicks on the menussecond for the touch inputs
                    if (!EventSystem.current.IsPointerOverGameObject(_touch.fingerId) && !pauseHandler.isPaused)
                    {
                        //move left
                        if (_touch.position.x < Screen.width / 2)
                        {
                            _rigid.velocity = new Vector2(-_speed, _jumpForce);
                        }
                        //move right
                        if (_touch.position.x > Screen.width / 2)
                        {
                            _rigid.velocity = new Vector2(_speed, _jumpForce);
                        }
                    }
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingTile") && !isDead)
        {
            isDead = true;
            //if best score was achieved this round
            if (score > bestScore)
            {
                bestScore = score;
                isBestScored = true;
            }
            balance += coins;
            SavePlayer();
            GameObject deathEffect = Instantiate(deathSplashEffect);
            deathEffect.transform.position = transform.position;

            gameObject.transform.GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(ShowDeathMenu());

        }

        if (collision.gameObject.CompareTag("Coin") && !isDead)
        {
            FindObjectOfType<AudioManager>().Play("CoinSound");
            if (collision.gameObject.name == "Bronze Coin(Clone)")
            {
                playerInfoUIManager.ShowTextUpdaterAnimation(collision.transform.position, 1);
                //ScoreUpdateTextAnimation.transform.position = new Vector3(0, Mathf.Lerp(collision.transform.position.y, collision.transform.position.y + 10, (Time.time) / 1.0f), 0);
                Destroy(collision.gameObject);
                playerInfoUIManager.CoinsUpdater(1);
            }
            if (collision.gameObject.name == "Silver Coin(Clone)")
            {
                playerInfoUIManager.ShowTextUpdaterAnimation(collision.transform.position, 2);
                Destroy(collision.gameObject);
                playerInfoUIManager.CoinsUpdater(2);
            }
            if (collision.gameObject.name == "Gold Coin(Clone)")
            {
                playerInfoUIManager.ShowTextUpdaterAnimation(collision.transform.position, 5);
                Destroy(collision.gameObject);
                playerInfoUIManager.CoinsUpdater(5);
            }
            if (collision.gameObject.name == "Blue Coin(Clone)")
            {
                playerInfoUIManager.ShowTextUpdaterAnimation(collision.transform.position, 10);
                Destroy(collision.gameObject);
                playerInfoUIManager.CoinsUpdater(10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreGate") && !isDead)
        {
            playerInfoUIManager.ScoreUpdater();
            Destroy(other.gameObject);
        }
    }

    IEnumerator ShowDeathMenu()
    {
        yield return new WaitForSeconds(1f);
        //Destroy(gameObject);

        deathMenu.ShowMenu(bestScore.ToString(), score.ToString(), balance.ToString());
        //DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("DeathMenu");
    }

    //void ScoreUpdater()
    //{
    //	switch (score)
    //	{
    //		default:
    //			SpawnRows.respawnDifficulty = 1.15f;
    //			break;
    //		case 20:
    //			SpawnRows.respawnDifficulty = 1.10f;
    //			break;
    //		case 25:
    //			SpawnRows.respawnDifficulty = 1.05f;
    //			break;
    //		case 30:
    //			ScrollBackground.speed = 1.7f;
    //			SpawnRows.respawnDifficulty = 1.0f;
    //			break;
    //		case 35:
    //			ScrollBackground.speed = 1.8f;
    //			_speed = 3.2f;
    //			_jumpForce = 2.9f;
    //			SpawnRows.respawnDifficulty = 0.95f;
    //			break;
    //		case 40:
    //			ScrollBackground.speed = 2.0f;
    //			_speed = 3.3f;
    //			_jumpForce = 3.1f;
    //			SpawnRows.respawnDifficulty = 0.9f;
    //			break;
    //	}
    //	//if (score == 10)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 1.15f;
    //	//}
    //	//else if (score == 20)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 1.10f;
    //	//}
    //	//else if (score == 25)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 1.05f;
    //	//}
    //	//else if (score == 30)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 1.0f;
    //	//}
    //	//else if (score == 35)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 0.95f;
    //	//}
    //	//else if (score == 40)
    //	//{
    //	//	SpawnRows.respawnDifficulty = 0.9f;
    //	//}
    //	score++;
    //	scoreText.text = score.ToString();
    //}

    //void CoinsUpdater(int moneyValue)
    //{
    //	coins += moneyValue;
    //	coinsText.text = coins.ToString();
    //}

    //void BestScoreUpdater()
    //{
    //	bestScoreText.text = "BEST: \n" + bestScore.ToString();
    //}
    #region SAVES
    public void SavePlayer()
    {
        //if the new score is better than the old one then save it and load it.
        SaveSystem.SavePlayer();
        LoadPlayer();
    }

    public void LoadPlayer()
    {
        //PlayerData data = SaveSystem.LoadPlayer();
        //bestScore = data.bestScore;
        //balance = data.balance;
        //activeLevelID = data.activeLevelID;
        //      boughtLevelID = data.boughtLevelID;
        SaveSystem.LoadAndAssign();

    }
    #endregion
}
