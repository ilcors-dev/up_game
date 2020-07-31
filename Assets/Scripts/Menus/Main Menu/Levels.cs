using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Levels List containing levels obj
public class Levels : IEquatable<Levels>
{
    public int levelID { get; set; }
    public GameObject levelPrefab { get; set; }
    public GameObject[] tilesPrefab { get; set; }
    public GameObject[] tilesPrefabEdges { get; set; }
    public GameObject floorPrefab { get; set; }
    public GameObject terrainPrefab { get; set; }
    public string levelName { get; set; }
    public GameObject levelPreviewImage { get; set; }
    public GameObject levelImage { get; set; }
    public bool boughtLevel { get; set; }
    public int levelCost { get; set; }
    public Color backgroundColor { get; set; }

    //public Levels GetObj(int otherLevelID)
    //{
    //	if (this.levelID == otherLevelID) return this;
    //	else return null;
    //}

    public bool Equals(Levels other)
    {
        if (other == null) return false;
        return (this.levelID.Equals(other.levelID));
    }
}
