using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private List<TileData> tiles = new List<TileData>();


    private int tileIndice;
    void Start()
    {
        //Initial Spawn
        SpawnTile(tiles[Random.Range(0, tiles.Count)]);
    }

    private void SpawnTile(TileData tileToBeSpawned)
	{
        GameObject newTile = Instantiate(tileToBeSpawned.tilePrefab, Vector3.zero, tileToBeSpawned.tilePrefab.transform.rotation);

        TileManager.worldTiles.Add(newTile);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            GenerateTile();
		}
	}

    private void GenerateTile()
	{
        SpawnTile(tiles[Random.Range(0, tiles.Count)]);

        //1. Pick Random Tile
        GameObject connectTile = TileManager.worldTiles[Random.Range(0, TileManager.worldTiles.Count)];

        //2. Get All Connection Possibilities
    }
}
