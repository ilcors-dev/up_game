using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    private Vector2 screenBounds;
    private GameObject FloorPrefab;//1x1 platform which destroys itself after the player jumps
    private GameObject TerrainPrefab;
    public GameObject NullObject;//default invisible bottom barrier to avoid infinite falling

    private int integerBoundX;
    private int integerBoundY;
    const int HILLS = 0;
    const int DESERT = 1;
    const int JUNGLE = 2;
    const int SPACE = 3;
    // Start is called before the first frame update
    void Start()
    {
        FloorPrefab = LevelCreator.levels[Player.activeLevelID].floorPrefab;
        TerrainPrefab = LevelCreator.levels[Player.activeLevelID].terrainPrefab;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        integerBoundX = Mathf.RoundToInt(screenBounds.x);
        integerBoundY = Mathf.RoundToInt(screenBounds.y);
        //PlaceTerrainRow();
        if (Player.activeLevelID == SPACE)
            PlaceTerrainSpace();
        else
        {
            PlaceTerrainSquare();
            AdjustTerrainOffset();//TOFIX!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    void PlaceTerrainRow()
    {
        for (float i = -(integerBoundX); i < integerBoundX + 1; i += 0.5f)
        {
            GameObject terrain = Instantiate(FloorPrefab) as GameObject;
            terrain.transform.position = new Vector3(i, -screenBounds.y, -6);
            terrain.transform.SetParent(GameObject.FindGameObjectWithTag("Terrain").transform);
        }
    }
    void PlaceTerrainSquare()
    {
        for (float i = -(integerBoundX) - 0.5f; i < integerBoundX + 1f; i += 0.5f)
        {
            for (float k = (-integerBoundY - 3); k < -integerBoundY; k += 0.5f)
            {
                if (k == (-integerBoundY - 0.5f))
                {
                    GameObject terrain = Instantiate(FloorPrefab) as GameObject;
                    terrain.transform.position = new Vector3(i, k, -6);
                    terrain.transform.SetParent(GameObject.FindGameObjectWithTag("Terrain").transform);
                }
                else
                {
                    GameObject terrain = Instantiate(TerrainPrefab) as GameObject;
                    terrain.transform.position = new Vector3(i, k, -6);
                    terrain.transform.SetParent(GameObject.FindGameObjectWithTag("Terrain").transform);
                }
            }
        }
    }
    //moves the terrain down by 1.5f, aka 3 blocks
    private void AdjustTerrainOffset()
    {
        GameObject t = GameObject.FindGameObjectWithTag("Terrain").gameObject;//first is main background
        t.transform.localPosition = new Vector3(t.transform.position.x, t.transform.position.y - 1.5f, t.transform.position.z);
    }

    void PlaceTerrainSpace()
    {
        GameObject terrain = Instantiate(NullObject, new Vector3(0, -5f, 0), Quaternion.identity) as GameObject;
        terrain.GetComponent<SpriteRenderer>().enabled = false;
        terrain.tag = "Terrain";
        for (float i = -(integerBoundX); i < integerBoundX + 1; i += 0.5f)
        {
            GameObject t = Instantiate(NullObject, new Vector3(i, -screenBounds.y - 3f, 0), Quaternion.identity) as GameObject;
            t.GetComponent<SpriteRenderer>().enabled = false;
            t.tag = "Terrain";
        }
    }
}
