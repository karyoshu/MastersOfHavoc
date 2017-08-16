using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretType
{
    Standard,
    MissileLauncher,
    LaserBeamer,
}

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public TurretType TurretType;
    public int cost;
}
