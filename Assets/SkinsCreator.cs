using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsCreator : MonoBehaviour
{
    public static List<Skins> skins = new List<Skins>();
    public GameObject[] skinsPrefabs;
    public GameObject[] SkinsImage;
    void Awake()
    {
        if (skins.Count == 0)
        {
            skins.Add(new Skins()
            {
                skinID = 0,
                skinPrefab = skinsPrefabs[0],
                skinImage = SkinsImage[0],
                boughtSkin = true,
                skinCost = 0
            });
            skins.Add(new Skins()
            {
                skinID = 1,
                skinPrefab = skinsPrefabs[1],
                skinImage = SkinsImage[1],
                boughtSkin = false,
                skinCost = 400
            });
        }
    }
}
