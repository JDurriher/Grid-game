using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string UnitName;     // For MenuManager
    public Tile OccupiedTile;   // Have on hero what tile they are on
    public Faction Faction;     // What faction unit belongs to
}
