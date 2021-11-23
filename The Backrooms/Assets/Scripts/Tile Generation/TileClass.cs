using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : BaseClass
{
	public TileData RandomTile(List<TileData> tiles)
	{
		return tiles[Random.Range(0, tiles.Count)];
	}

	public static void DestroyConnectionPoint(WorldTile connectPointTile, Transform connectPoint)
	{
		Destroy(connectPoint.gameObject);
		connectPointTile.connectionPoints.Remove(connectPoint);
	}
}
