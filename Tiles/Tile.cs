using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour      // Abstract modifier indicates class is intended only to be a base clase of other classes, not instantiated on its own
{
    public string tileName;
    [SerializeField] protected SpriteRenderer tileRenderer;     // Protected same as private but derived scripts can access
    [SerializeField] private GameObject highlight;  // Create reference for highlighting tile when mouse hovers over
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => isWalkable && OccupiedUnit == null; // Final check to make sure tile is walkable. Walkable if isWalkable is true and OccupiedUnit equals null. Then can just call this function and check if tile is walkable

    // This logic will run on all tiles, but each individual tile has the chance to override this function and do specific logic for that tile
    // Virtual keyword modifies method to allow for it to be overriden in a derived class.
    public virtual void Init(int x, int y)
    {
      
    }

    void OnMouseEnter() // Enable/disable highlight GameObject whenever mouse over
    {
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);

    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.HeroesTurn) return; // Checking if it's plauyers turn. If not ignore

        if (OccupiedUnit != null)    // Any logic beyond this means there is a unit on selection
        {
            if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit); // If tile selected has a hero let's select it
            else
            {
                if (UnitManager.Instance.SelectedHero != null)      // Attacking as unit selected is an enemy
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    // This is where we compare ranks to determine winner
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null); // De-selects unit
                }
            }
        }
        else
        {
            if(UnitManager.Instance.SelectedHero != null)   // Got a selected unit and clicking on a tile that doesn't have a unit on it
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null); // De-selects unit
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;   // Going to units occuiped tile and setting occuipied unit to null
        unit.transform.position = transform.position;    // Places hero in tile
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

}
