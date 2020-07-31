using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class SpawnRows : MonoBehaviour
{
    //public GameObject TilePrefab;
    public GameObject[] coinsPrefabs;

    public GameObject ScoreGatePrefab;//invisible object used to detect when the player passes a row
    //public float respawnTime = 1.0f;
    [SerializeField]
    public static float spawnCoinDelay;// = 1.35f;//the higher it is the more distant the rows will be making it easier to avoid
    public static float spawnDifficultyDistance;// = 2.7f;//value in distance between rows, lower is harder
    [SerializeField]
    public static float fallingSpeed;// = 2.6f;//modify the speed of the falling things
    public static int meteoriteWaveCount;// = 4;
    //public GameObject[] scrollingBg;

    private Vector2 screenBounds;
    //higher int values of the screen bound, ex: 2.8f = 3
    private int integerBoundX;
    public static int integerBoundY;

    int[] row;
    //until the first row isn't spawned coin won't spawn
    //private bool spawnCoin = false;
    // Start is called before the first frame update
    private GameObject lastTile = null;

    public GameObject[] meteoritePrefabs;
    private GameObject[] selectedLevelTilesPrefabs;
    private GameObject[] selectedLevelTilesPrefabsEdges;
    void Start()
    {
        spawnCoinDelay = 1.3f;
        spawnDifficultyDistance = 2.8f;
        fallingSpeed = 2.6f;
        meteoriteWaveCount = 4;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        integerBoundX = Mathf.RoundToInt(screenBounds.x);
        integerBoundY = Mathf.RoundToInt(screenBounds.y);
        StartCoroutine(CoinWave());
        //StartCoroutine(TileWave());
        if (Player.activeLevelID == 3)//space Level
        {
            StartCoroutine(MeteoriteWave());
        }
        selectedLevelTilesPrefabs = LevelCreator.levels[Player.activeLevelID].tilesPrefab;
        selectedLevelTilesPrefabsEdges = LevelCreator.levels[Player.activeLevelID].tilesPrefabEdges;
    }
    private bool firstSpawned = false;
    private void LateUpdate()
    {
        Vector2 tmp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (integerBoundY < Mathf.RoundToInt(tmp.y))
        {
            screenBounds = tmp;
            integerBoundY = Mathf.RoundToInt(screenBounds.y);
        }

        if (Player.activeLevelID != 3)//space Level
        {
            if (!firstSpawned)
            {
                firstSpawned = true;
                SpawnTile();
            }
            //float distance = Vector3.Distance(new Vector3(0, lastRowPosY, 0), new Vector3(0, lastTile.gameObject.transform.position.y, 0));
            if (!Player.isDead)
            {
                float distance = Mathf.Abs(screenBounds.y - lastTile.gameObject.transform.position.y);
                if (distance >= spawnDifficultyDistance && lastTile.gameObject.transform.position.y <= screenBounds.y * 2)
                {
                    //spawns the rows 3 times so that they do not appear suddenly on the screen.
                    for (int i = 1; i <= 3; i++)
                    {
                        SpawnTile();
                    }
                }
            }
        }
    }
    private List<int> GenRow()
    {
        //int nPrefabs = (integerBoundX * 3);
        const int lastIndexOffest = 3;//to avoid array out of bound Exception
        //int[] comp = new int[nPrefabs];
        //int i = UnityEngine.Random.Range(0, nPrefabs - lastIndexOffest);
        ////      int[] comp = new int[(integerBoundX * 4) + 2];
        ////      int i = UnityEngine.Random.Range(0, (integerBoundX * 4) - 3);
        //comp[i] = 1;
        //for (int j = 0; j < comp.Length; j++)
        //{
        //    if (comp[j] == 1)
        //    {
        //        comp[j] = 1;
        //        comp[j + 1] = 1;
        //        comp[j + 2] = 1;
        //        comp[j + 3] = 1;
        //        j += 3;
        //    }
        //    else comp[j] = 0;
        //}
        List<int> comp = new List<int>();
        int randIndex;
        //randIndex = UnityEngine.Random.Range(-(integerBoundX), integerBoundX);
        int estimatedBlocks = (int)(screenBounds.x * 2 / 0.5f) + 1;//to avoid empty spaces at the end or at the start
        int[] cells = new int[estimatedBlocks];
        do
        {
            randIndex = UnityEngine.Random.Range(0, estimatedBlocks);
        } while (randIndex + 3 > estimatedBlocks);

        for (int i = 0; i < randIndex; i++)
        {
            comp.Add(0);
        }
        int c = 0;
        for (int i = randIndex; i < 30; i++)
        {
            if (c != 4)
            {
                c++;
                comp.Add(1);
            }
            else comp.Add(0);
        }
        return comp;
        //if (randIndex + 1f > integerBoundX)
        //{
        //    for (float i = (integerBoundX) + 1; i > randIndex+.5f; i -= .5f)
        //    {
        //        if (i != randIndex - 1) comp.Add(0);
        //    }
        //    int c = 0;
        //    for (float i = randIndex+.5f; i > -integerBoundX - 1; i -= .5f)
        //    {
        //        //(c != 4) ? comp.Add(1) : comp.Add(0);
        //        if (c != 4)
        //        {
        //            c++;
        //            comp.Add(1);
        //        }
        //        else comp.Add(0);
        //    }
        //}
        //else
        //{
        //for (float i = -(integerBoundX) - .5f; i < randIndex +.5f; i += .5f)
        //    {
        //        if (i != randIndex - 1) comp.Add(0);
        //    }
        //    int c = 0;
        //    for (float i = randIndex-.5f; i < integerBoundX + .5f; i += .5f)
        //    {
        //        //(c != 4) ? comp.Add(1) : comp.Add(0);
        //        if (c != 4)
        //        {
        //            c++;
        //            comp.Add(1);
        //        }
        //        else comp.Add(0);
        //    }
        //}
        //return comp;
    }

    private void SpawnTile()//STABLE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        //List<int> row = GenRow();
        //row = GenRow();
        //int estimatedBlocks = (int)(screenBounds.x * 2 / 0.5f) + 1;//to avoid empty spaces at the end or at the start
        //int[] cells = new int[estimatedBlocks];
        int randIndex;
        //do
        //{
        randIndex = (int)UnityEngine.Random.Range(-screenBounds.x - 0.5f, screenBounds.x - 1.5f);//SE L'INDICE RAMDOM è FUORI DALLO SCHERMO SCREENBOUNDS-Y AGGIUNGI .5 FINCHE NON VA BENE
        //} while (randIndex < -screenBounds.x || randIndex + 1.5f > screenBounds.x);
        int j = 0;
        if (lastTile == null)
            SpawnGatePoint(screenBounds.y);
        else
            SpawnGatePoint(lastTile.transform.position.y + (spawnDifficultyDistance));

        GameObject rowTile = null;
        for (float i = -integerBoundX - .5f; i <= integerBoundX + .5f; i += 0.5f)
        {
            if (i != randIndex)//spawn common level tiles
            {
                int tileID = Random.Range(0, selectedLevelTilesPrefabs.Length);
                GameObject randomLevelPrefab = selectedLevelTilesPrefabs[tileID];
                if (lastTile == null)
                {
                    //edges are save in this format [left, left, ..., right, right, ...]
                    if (i + 0.5f == randIndex)//right
                        rowTile = Instantiate(selectedLevelTilesPrefabsEdges[Random.Range(3, 6)]) as GameObject;
                    else if (i - 0.5f == randIndex + 1.5f)//left
                        rowTile = Instantiate(selectedLevelTilesPrefabsEdges[Random.Range(0, 3)]) as GameObject;
                    else
                        rowTile = Instantiate(randomLevelPrefab) as GameObject;
                    rowTile.tag = "FallingTile";
                    rowTile.transform.position = new Vector3(i, screenBounds.y, -5);
                    rowTile.transform.SetParent(GameObject.Find("Falling Tiles").transform);
                }
                else//spawns three at once so that they do not appear suddently on screen
                {
                    //edges are save in this format [left, left, ..., right, right, ...]
                    if (i + 0.5f == randIndex)//right
                        rowTile = Instantiate(selectedLevelTilesPrefabsEdges[Random.Range(3, 6)]) as GameObject;
                    else if (i - 0.5f == randIndex + 1.5f)//left
                        rowTile = Instantiate(selectedLevelTilesPrefabsEdges[Random.Range(0, 3)]) as GameObject;
                    else
                        rowTile = Instantiate(randomLevelPrefab) as GameObject;
                    rowTile.tag = "FallingTile";
                    rowTile.transform.position = new Vector3(i, lastTile.gameObject.transform.position.y + (spawnDifficultyDistance), -5);
                    rowTile.transform.SetParent(GameObject.Find("Falling Tiles").transform);
                }
            }
            else i += 1.5f;

            j++;
        }
        lastTile = rowTile;
    }
    //object that is basically a line to keep track of the score. Acts like a gate, when the players surpasses it +1 to the score.
    private void SpawnGatePoint(float spawnPosY)
    {
        GameObject scoreGate = Instantiate(ScoreGatePrefab) as GameObject;
        scoreGate.tag = "ScoreGate";
        scoreGate.transform.position = new Vector3(0, spawnPosY, -5);
        scoreGate.transform.localScale = new Vector3(integerBoundX * 2, 0.5f, 0);
    }

    /**
	 * Spawns coin.
	 * Before spawning checks the expected spawn point, if in that position there's another object the algorithm generates another position.
	 * This happens until the generated position is not the one of another object
	 */
    private readonly int maxSpawnAttemptsPerRow = 13;
    private void SpawnCoin()
    {
        // How many times we've attempted to spawn this obstacle
        int spawnAttempts = 0;

        // whether or not we can spawn in this position
        bool validPosition = false;

        // Create a position variable, then set it to random
        Vector2 spawnPos = new Vector2(0, 0);

        //While we don't have a valid position 
        //and we haven't tried spawning this obstable too many times
        while (validPosition == false && spawnAttempts < maxSpawnAttemptsPerRow)
        {
            //Increase our spawn attempts
            spawnAttempts++;
            //Pick a random position
            spawnPos = new Vector2(UnityEngine.Random.Range(-integerBoundX, integerBoundX), screenBounds.y);
            //This position is valid until proven invalid
            validPosition = true;
            //Collect all colliders within our Obstacle Check Radius
            //https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircleAll.html
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, .5f);
            //Go through each collider collected
            foreach (Collider2D col in colliders)
            {
                //If this collider is tagged "Obstacle", the falling tiles have this tag
                if (col.tag == "FallingTile")
                {
                    //Then this position is not a valid spawn position
                    validPosition = false;
                }
            }
        }
        //If we exited the loop with a valid position
        if (validPosition)
        {
            GameObject coin = Instantiate(CoinSpawnChance()) as GameObject;
            //Spawn the obstacle here
            //coin = Instantiate(BronzeCoinPrefab) as GameObject;
            coin.GetComponent<Rigidbody2D>().isKinematic = false;
            Collider2D[] terrainTiles = GameObject.Find("Terrain").GetComponentsInChildren<Collider2D>();
            for (int i = 0; i < terrainTiles.Length; i++)
            {
                Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), terrainTiles[i]);
                //Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), scrollingBg[0].GetComponent<BoxCollider2D>());
                //Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), scrollingBg[1].GetComponent<BoxCollider2D>());
            }
            coin.tag = "Coin";
            coin.transform.position = new Vector3(spawnPos.x, integerBoundY, -5);
        }
    }
    /**
	 * Returns the coin prefab to instantiate based on the score of the player and the value of the coins
	 * 
	 * */
    private GameObject CoinSpawnChance()
    {
        int score = Player.score;
        float spawnProb = Random.value;
        GameObject tmp = null;
        if (score < 10)
        {
            if (spawnProb < 0.01f)//1% chance of spawning a DIAMOND coin
                tmp = coinsPrefabs[3];
            else if (spawnProb < 0.05f)//5% chance of spawning a DIAMOND coin
                tmp = coinsPrefabs[2];
            else if (spawnProb < 0.20)//10% chance of spawning a silver coin
                tmp = coinsPrefabs[1];
            else
                tmp = coinsPrefabs[0];
        }
        if (score < 20 && score >= 10)
        {
            if (spawnProb < 0.05f)//5% chance of spawning a DIAMOND coin
                tmp = coinsPrefabs[3];
            else if (spawnProb < 0.25f)//25% chance of spawning a GOLD coin
                tmp = coinsPrefabs[2];
            else if (spawnProb < 0.40f)//40% chance of spawning a SILVER coin
                tmp = coinsPrefabs[1];
            else
                tmp = coinsPrefabs[0];
        }
        if (score < 35 && score >= 20)
        {
            if (spawnProb < 0.10f)//10% chance of spawning a DIAMOND coin
                tmp = coinsPrefabs[3];
            else if (spawnProb < 0.35f)//35% chance of spawning a GOLD coin
                tmp = coinsPrefabs[2];
            else if (spawnProb < 0.60f)//60% chance of spawning a SILVER coin
                tmp = coinsPrefabs[1];
            else
                tmp = coinsPrefabs[0];
        }
        if (score >= 35)
        {
            if (spawnProb < 0.15f)//15% chance of spawning a DIAMOND coin
                tmp = coinsPrefabs[3];
            else if (spawnProb < 0.50f)//45% chance of spawning a GOLD coin
                tmp = coinsPrefabs[2];
            else if (spawnProb < 0.80f)//80% chance of spawning a SILVER coin
                tmp = coinsPrefabs[1];
            else
                tmp = coinsPrefabs[0];
        }
        return tmp;
    }

    //IEnumerator TileWave()
    //{
    //	while (true)
    //	{
    //		yield return new WaitForSeconds(respawnTime * respawnDifficulty);
    //		SpawnTile();
    //	}
    //}
    IEnumerator CoinWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCoinDelay);
            SpawnCoin();
        }
    }

    IEnumerator MeteoriteWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.7f);
            SpawnMeteoritesWave();
        }
    }

    private void SpawnMeteoritesWave()
    {
        int spawnAttempts = 0;

        // whether or not we can spawn in this position
        bool validPosition = false;

        // Create a position variable, then set it to random
        Vector2 spawnPos = new Vector2(0, 0);
        for (int i = 0; i < meteoriteWaveCount; i++)
        {
            //While we don't have a valid position 
            //and we haven't tried spawning this obstable too many times
            while (validPosition == false && spawnAttempts < maxSpawnAttemptsPerRow)
            {
                //Increase our spawn attempts
                spawnAttempts++;
                //Pick a random position
                spawnPos = new Vector2(UnityEngine.Random.Range(-screenBounds.x, screenBounds.x), UnityEngine.Random.Range(screenBounds.y, screenBounds.y + 3f));
                //This position is valid until proven invalid
                validPosition = true;
                //Collect all colliders within our Obstacle Check Radius
                //https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircleAll.html
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, .5f);
                //Go through each collider collected
                foreach (Collider2D col in colliders)
                {
                    //If this collider is tagged "Obstacle", the falling tiles have this tag
                    if (col.tag == "FallingTile")
                    {
                        //Then this position is not a valid spawn position
                        validPosition = false;
                    }
                }
            }
            if (validPosition)
            {
                float randomPosX = Random.Range(-screenBounds.x, screenBounds.x);
                float randomPosY = Random.Range(screenBounds.y, screenBounds.y + 3f);
                GameObject tmp = Instantiate(meteoritePrefabs[Random.Range(0, meteoritePrefabs.Length)]);
                tmp.tag = "FallingTile";
                tmp.transform.position = new Vector3(randomPosX, randomPosY, -5);
            }
        }
        SpawnGatePoint((screenBounds.y + (screenBounds.y + 3f)) / 2);
    }
}
