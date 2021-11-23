using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : BaseClass
{
	public TileData RandomTilePrefab(List<TileData> tiles)
	{
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
