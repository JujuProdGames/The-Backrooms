using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
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

	[SerializeField] private List<Vector3> wtp = new List<Vector3>();
	private void Update()
	{
		wtp = worldTilePositions;
	}
}
