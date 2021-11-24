using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : BaseClass
{
	public TileData RandomTilePrefab(TileSpawnRate tileRate)
	{
		float e = Random.Range(1, 101);

		Debug.Log("RNG:" + e);
		for (int i = 0; i < tileRate.criticalValues.Count; i++)
		{
			Debug.Log("Current Critical Point" + tileRate.criticalValues[i]);
			if (e >= tileRate.criticalValues[i] && e < tileRate.criticalValues[i + 1])
			{
				Debug.Log("Spawning " + tileRate.tileRates[i].tile);
				return tileRate.tileRates[i].tile;
			}
		}

		Debug.Log("nothing returned");
		return tileRate.tileRates[Random.Range(0, tileRate.tileRates.Count)].tile;
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
