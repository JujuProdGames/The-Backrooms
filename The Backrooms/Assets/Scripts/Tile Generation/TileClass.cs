using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : BaseClass
{
	public TileData RandomTilePrefab(List<TileData> tiles, TileSpawnRate tileRate)
	{
		/*float e = Random.Range(1, 101);
		float criticalValue = 0;
		List<float> criticalPoints = new List<float>();

		for (int i = 0; i < tileRate.tiles.Count; i++)
		{
			criticalValue += tileRate.tiles[i].spawnChance * 100;
			criticalPoints.Add(criticalValue);
		}

		for (int i = 0; i < tileRate.tiles.Count; i++)
		{
			if (e >= criticalPoints[i] && e < criticalPoints[i + 1])
				return tileRate.tiles[i].tile;
		}*/

		Debug.Log("nothing returned");
		return tiles[Random.Range(0, tiles.Count)];
	}

	public Vector3 RandomTileRotation()
	{
		return new Vector3(-90, Random.Range(0, 4) * 90, 0);
	}

	public static void DestroyConnectionPoint(WorldTile connectPointTile, Transform connectPoint)
	{
		connectPointTile.connectionPoints.Remove(connectPoint);
		Destroy(connectPoint.gameObject);
	}

	public static void ClearConnectionPoints(WorldTile tile)
	{
		foreach (Transform point in tile.connectionPoints) Destroy(point.gameObject);
		tile.connectionPoints.Clear();
	}
}
