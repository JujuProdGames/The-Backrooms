using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : BaseClass
{
	public TileData RandomTilePrefab(TileSpawnRate tileRate)
	{
		float e = Random.Range(1, 101);

		for (int i = 0; i < tileRate.criticalValues.Count; i++)
		{
			if (e >= tileRate.criticalValues[i] && e <= tileRate.criticalValues[i + 1])
			{
				return tileRate.tileRates[i].tile;
			}
		}

		Debug.LogError("nothing returned lelelelelelelelrelelelel");
		return tileRate.tileRates[Random.Range(0, tileRate.tileRates.Count)].tile;
	}

	public Vector3 RandomTileRotation()
	{
		return new Vector3(-90, Random.Range(0, 4) * 90, 0);
	}

	public static void DestroyConnectionPoint(TileWorld connectPointTile, Transform connectPoint)
	{
		connectPointTile.connectionPoints.Remove(connectPoint);
		Destroy(connectPoint.gameObject);
	}

	public static void ClearConnectionPoints(TileWorld tile)
	{
		foreach (Transform point in tile.connectionPoints) Destroy(point.gameObject);
		tile.connectionPoints.Clear();
	}

	public static void LoadTile(TileWorld tile)
	{
		tile.isActive = true;
		foreach (MeshRenderer gfx in tile.GetComponentsInChildren<MeshRenderer>())
		{
			gfx.enabled = true;
		}
	}

	public static void UnloadTile(TileWorld tile)
	{
		tile.isActive = false;
		foreach (MeshRenderer gfx in tile.GetComponentsInChildren<MeshRenderer>())
		{
			gfx.enabled = false;
		}
	}
}
