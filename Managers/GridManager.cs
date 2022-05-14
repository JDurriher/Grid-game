using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width, height;     // Use SerializeField when variable needs to be private but also want it to show in Editor. Making field serializable allows Unity to save and load those values even though they are not public

    [SerializeField] private Tile grassTile, mountainTile; // Reference to various grid tiles

    [SerializeField] private Transform cam; // Referencing camera so we can center camera on generated grid

    // How to grab tiles in logic
    private Dictionary<Vector2, Tile> tiles;


    private void Awake()
    {
        Instance = this;
    }



    public void GenerateGrid() // Loop over width and height. Corresponds to GameManager object dimensions
    {
        tiles = new Dictionary<Vector2, Tile>();    // Instantiate dictionary when creating grid
        for (int x = 0; x < width; x++)         // Generate width
        {
            for (int y = 0; y < height; y++)    // Generate height
            {
                var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile; // Randomising placement of tiles. If value between 0 and 6 is equal to 3, set to mountain tile, otherwise set to grass tile.
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";   // Naming so it's clear in editor. $ is for string interpolation - output is "Tile x y" as the integers x and y are fed into the string.

                spawnedTile.Init(x, y);

                tiles[new Vector2(x, y)] = spawnedTile;

            }
        }

        cam.transform.position = new Vector3((float) width/2 -0.5f, (float)height/2 -0.5f, -10);    // Position camera at width/height of the map (grid) divided by 2 and remove half of a tile. Values are ints hence conversion to float.

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);

    }

    public Tile GetHeroSpawnTile()
    {
        return tiles.Where(t => t.Key.x < width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;  // Check tile is on left side of map and that it is walkable. And also randomise spawn tile
    }

    public Tile GetEnemySpawnTile()
    {
        return tiles.Where(t => t.Key.x > width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;  // Check tile is on right side of map and that it is walkable. And also randomise spawn tile
    }

    public Tile GetTileAtPosition(Vector2 pos)  // Function returning tile at certain vector. 
    {
        if (tiles.TryGetValue(pos, out var tile))   // If the tile is available, return tile otherwise null
        {                                           // Allows us to call function and get tile at current position
            return tile;
        }

        return null;
    }
}
