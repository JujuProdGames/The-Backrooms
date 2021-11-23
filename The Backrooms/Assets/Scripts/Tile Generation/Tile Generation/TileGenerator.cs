using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : TileClass
{
    [SerializeField] private List<TileData> tiles = new List<TileData>();

    void Start()
    {
        //Initial Spawn
        SpawnTile(RandomTilePrefab(tiles), null, new Vector3(0,0,0), RandomTileRotation());
    }

	private void SpawnTile(TileData tileToBeSpawned, WorldTile neighbouringTile, Vector3 position, Vector3 rotation)//ADD POSITION
	{
		GameObject newTileGameObject = Instantiate(tileToBeSpawned.tilePrefab, position, Quaternion.Euler(rotation));
        WorldTile newTile = newTileGameObject.GetComponent<WorldTile>();

		newTile.neighboringTile = neighbouringTile;

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
		//1. Pick Random Tile
		WorldTile connectTile = TileManager.worldTiles[Random.Range(0, TileManager.worldTiles.Count)];

		//1.5 Repick Tile if No Points Available
		while(connectTile.connectionPoints.Count == 0)
		{
			connectTile = TileManager.worldTiles[Random.Range(0, TileManager.worldTiles.Count)];
		}

		//2. Pick Random Connection from Tile
		Transform connectPoint = connectTile.connectionPoints[Random.Range(0, connectTile.connectionPoints.Count)];

		//3. Spawn Tile at Connection Position
		SpawnTile(RandomTilePrefab(tiles), connectTile, connectPoint.position, RandomTileRotation());

		//4. Remove Illegal Connections (2 Points; 1 per Tile -=- Destroy Existing Point, Other Won't be Spawned In)
		DestroyConnectionPoint(connectTile, connectPoint);
	}	
}
