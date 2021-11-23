using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private List<TileData> tiles = new List<TileData>();


    private int tileIndice;
    void Start()
    {
        //Initial Spawn
        SpawnTile(RandomTile(), new Vector3(0,0,0));
    }

    private void SpawnTile(TileData tileToBeSpawned, Vector3 position)//ADD POSITION
	{
        GameObject newTileGameObject = Instantiate(tileToBeSpawned.tilePrefab, position, tileToBeSpawned.tilePrefab.transform.rotation);
        WorldTile newTile = newTileGameObject.GetComponent<WorldTile>();

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
		SpawnTile(RandomTile(), connectPoint.position);

		//4. Remove Illegal Connections (2 Points; 1 per Tile -=- Destroy Existing Point, Other Won't be Spawned In)
		DestroyConnectionPoint(connectTile, connectPoint);
	}

	private static void DestroyConnectionPoint(WorldTile connectTile, Transform connectPoint)
	{		
		Destroy(connectPoint.gameObject);
		connectTile.connectionPoints.Remove(connectPoint);
	}

	private TileData RandomTile()
	{
        return tiles [Random.Range(0, tiles.Count)];
    }
}
