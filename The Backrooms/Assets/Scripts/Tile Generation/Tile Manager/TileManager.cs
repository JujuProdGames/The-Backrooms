using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : BaseClass
{
	#region Singleton
	private static TileManager instance;
	private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(gameObject);
	}
    #endregion

    public static List<WorldTile> worldTiles = new List<WorldTile>();
	public static List<Vector3> worldTilePositions
	{
		get{
			//1. Initialize List
			List<Vector3> worldTilePos = new List<Vector3>();

			//2. Add Positions Based On Tiles
			foreach(WorldTile tile in worldTiles)
			{
				//Vector3 convertedTilePos = new Vector3(tile.transform.position.x, tile.transform.position.z, 0);
				worldTilePos.Add(tile.transform.position);
			}

			//3. Return Value
			return worldTilePos;
		}		
	}

	public static List<Vector3> connectionPositions
	{		
		get
		{
			//1. Initialize List
			List<Vector3> connectionPos = new List<Vector3>();

			//2. Get All Tiles w/ Connection Points
			foreach (WorldTile tile in worldTiles)
			{
				if (tile.connectionPoints.Count != 0)
				{
					foreach(Transform connectPoint in tile.connectionPoints)
					{
						//3. Add Connect Point to List
						connectionPos.Add(connectPoint.position);
					}
				}
			}

			//4. Return Value
			return connectionPos;
		}
	}

	[SerializeField] private List<Vector3> ctp = new List<Vector3>();
	private void Update()
	{
		ctp = connectionPositions;
	}
}
