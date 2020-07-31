using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : IEquatable<Skins>
{
    public int skinID { get; set; }
    public GameObject skinPrefab { get; set; }
    public GameObject skinImage { get; set; }
    //public GameObject deathPrefab { get; set; }
    public bool boughtSkin { get; set; }
    public int skinCost { get; set; }
    public bool Equals(Skins other)
    {
        throw new NotImplementedException();
    }
}
