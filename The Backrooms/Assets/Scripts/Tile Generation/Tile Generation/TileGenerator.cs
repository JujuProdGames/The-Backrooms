using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : TileClass
{
	[Header("Probabilty of Tiles Spawning")]
	[SerializeField] private TileSpawnRate tileRate;

	#region Singleton
	public static TileGenerator Instance;
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}
	#endregion

	public void SpawnFirstTile()
	{
		//Initial Spawn
		SpawnTile(RandomTilePrefab(tileRate), null, new Vector3(0, 0, 0), RandomTileRotation());
	}

	private void SpawnTile(TileData tileToBeSpawned, TileWorld neighbouringTile, Vector3 position, Vector3 rotation)//ADD POSITION
	{
		GameObject newTileGameObject = Instantiate(tileToBeSpawned.tilePrefab, position, Quaternion.Euler(rotation));
        TileWorld newTile = newTileGameObject.GetComponent<TileWorld>();

		newTile.neighboringTile = neighbouringTile;

        TileManager.worldTiles.Add(newTile);
	}

	public void GenerateTile()
	{
		//1. Pick Random Tile
		TileWorld connectTile = TileManager.worldTiles[Random.Range(0, TileManager.worldTiles.Count)];

		//1.5 Repick Tile if No Points Available -- CAN CAUSE CRASH IF NO MORE SPOTS ARE AVAILABLE!
		if (TileManager.connectionPositions.Count > 0)
		{
			while (connectTile.connectionPoints.Count == 0)
			{
				connectTile = TileManager.worldTiles[Random.Range(0, TileManager.worldTiles.Count)];
			}
		}
		else
		{
			Debug.LogError("No More Connection Spots Can Be Found. sadge");
			return;
		}

		//2. Pick Random Connection from Tile
		Transform connectPoint = connectTile.connectionPoints[Random.Range(0, connectTile.connectionPoints.Count)];

		//3. Spawn Tile at Connection Position
		SpawnTile(RandomTilePrefab(tileRate), connectTile, connectPoint.position, RandomTileRotation());

		//4. Remove Illegal Connections (2 Points; 1 per Tile -=- Destroy Existing Point, Other Won't be Spawned In)
		DestroyConnectionPoint(connectTile, connectPoint);
	}	
}
