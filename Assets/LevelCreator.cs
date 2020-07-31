using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    public static List<Levels> levels = new List<Levels>();
    public GameObject[] levelsPrefab;
    public GameObject[] levelsImage;
    public GameObject[] levelsImagePreview;

    public GameObject defaultFloor;


    public GameObject[] hillsPrefabs;
    public GameObject[] hillsPrefabsEdges;
    public GameObject hillsFloor;
    public GameObject hillsTerrain;


    public GameObject[] desertPrefabs;
    public GameObject[] desertPrefabsEdges;
    public GameObject desertFloor;
    public GameObject desertTerrain;


    public GameObject[] junglePrefabs;
    public GameObject[] junglePrefabsEdges;
    public GameObject jungleFloor;
    public GameObject jungleTerrain;
    public GameObject[] meteoritePrefabs;
    private static Color defaultUnityColor = new Color(49 / 255f, 77 / 255f, 121 / 255f, 1);
    Color[] bgColors = { defaultUnityColor, new Color(160 / 255f, 248 / 255f, 255 / 255f),
        new Color(155 / 255f, 136 / 255f, 77 / 255f), defaultUnityColor, new Color(0, 0, 0, 1) };
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;//makes the game try to get the highest fps possible
        if (levels.Count == 0)
        {
            levels.Add(new Levels()
            {
                levelID = 0,
                levelName = "Hills",
                levelPrefab = levelsPrefab[0],
                tilesPrefab = hillsPrefabs,
                tilesPrefabEdges = hillsPrefabsEdges,
                floorPrefab = hillsFloor,
                terrainPrefab = hillsTerrain,
                levelPreviewImage = levelsImagePreview[0],
                levelImage = levelsImage[0],
                boughtLevel = true,
                levelCost = 0,
                backgroundColor = new Color(160 / 255f, 248 / 255f, 255 / 255f)
            });
            levels.Add(new Levels()
            {
                levelID = 1,
                levelName = "Desert",
                levelPrefab = levelsPrefab[1],
                tilesPrefab = desertPrefabs,
                tilesPrefabEdges = desertPrefabsEdges,
                floorPrefab = desertFloor,
                terrainPrefab = desertTerrain,
                levelPreviewImage = levelsImagePreview[1],
                levelImage = levelsImage[1],
                boughtLevel = false,//boughtLevel = false,
                levelCost = 500,
                backgroundColor = defaultUnityColor
            });
            levels.Add(new Levels()
            {
                levelID = 2,
                levelName = "Jungle",
                levelPrefab = levelsPrefab[2],
                tilesPrefab = junglePrefabs,
                tilesPrefabEdges = junglePrefabsEdges,
                floorPrefab = jungleFloor,
                terrainPrefab = jungleTerrain,
                levelPreviewImage = levelsImagePreview[2],
                levelImage = levelsImage[2],
                boughtLevel = true,//boughtLevel = false,
                levelCost = 1000,
                backgroundColor = defaultUnityColor
            });
            levels.Add(new Levels()
            {
                levelID = 3,
                levelName = "Space",
                levelPrefab = levelsPrefab[3],
                levelPreviewImage = levelsImagePreview[3],
                levelImage = levelsImage[3],
                boughtLevel = true,//boughtLevel = false,
                levelCost = 2000,
                backgroundColor = new Color(0, 0, 0, 1)
            });
        }
    }
}
