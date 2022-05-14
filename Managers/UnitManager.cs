using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnit> units;

    public BaseHero SelectedHero;       // For movement. Only want to be able to select own units

    void Awake()
    {
        Instance = this;

        // Go into resources folder, look through all the sub folders for any type of scriptable unit and put it into 'units' list 
        units = Resources.LoadAll<ScriptableUnit>("Units").ToList(); // Of type scriptable units and look in Units folder
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;

        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);   // Get random unit of type base hero of faction hero
            var spawnedHero = Instantiate(randomPrefab);                // Now we have unit we spawn it in (on one half of grid)
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();  // Now we grab a random spawn tile

            randomSpawnTile.SetUnit(spawnedHero);   // Calling function in Tile script
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);

    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);   // Get random unit of type base hero of faction hero
            var spawnedEnemy = Instantiate(randomPrefab);                // Now we have unit we spawn it in (on one half of grid)
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();  // Now we grab a random spawn tile

            randomSpawnTile.SetUnit(spawnedEnemy);   // Calling function in Tile script
        }

        GameManager.Instance.ChangeState(GameState.HeroesTurn);

    }

    // Randomly grab hero from unit list
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit  // Generic function: takes in a faction (hero or enemy?) where T is BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab; // Some lambda shit? Go through units, want all units in faction we are passing in (line 31). Randomly shuffle list around and selecting first one from randomised stack and returning just the prefab.
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

}
