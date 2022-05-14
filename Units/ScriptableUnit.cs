using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]  // Way of right clicking in project pane and creat new unit

public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab; // Just needs type BaseUnit
}

public enum Faction
{
    Hero = 0,
    Enemy = 1
}