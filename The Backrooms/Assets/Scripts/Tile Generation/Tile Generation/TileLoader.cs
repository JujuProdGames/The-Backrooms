using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : TileClass
{
	[SerializeField] private TileGenerator tg;
	[SerializeField] private TileData firstTile;
	List<TileWorld> tilesInRange
	{
		get
		{
			List<TileWorld> tilesInRange = new List<TileWorld>();
			List<Collider> tilesInRangeCol = new List<Collider>(Physics.OverlapSphere(Player.Instance.transform.position, loadRadius, tileMask));

			foreach (Collider tileCol in tilesInRangeCol)
			{
				tilesInRange.Add(tileCol.GetComponent<TileWorld>());
			}

			return tilesInRange;
		}
	}

	[Header("Tile Data")]
	[SerializeField] private LayerMask tileMask;
	[SerializeField] private float tileDiameter = 3f;

	[Header("Initial Spawn")]
	[Range(100, 5000)]
	[SerializeField] private float initialTiles = 5000;

	[Header("tiles go BRRRRRRRRRRRR")]
	[Range(0, .25f)]
	[SerializeField] private float timeBtwnSpawns = .01f;
	float t;

	private Vector3 currentPlayerRoom
	{
		get
		{
			return new Vector3(
				Mathf.FloorToInt(Player.Instance.transform.position.x / tileDiameter) * tileDiameter,
				0,
				Mathf.FloorToInt(Player.Instance.transform.position.z / tileDiameter) * tileDiameter);
		}
	}

	private Vector3 comparePlayerRoom;

	[Header("Generate Tiles Around Player")]
	[Range(50, 300)]
	[SerializeField] private float loadRadius = 100;

	[Range(0, 1)]
	[SerializeField] private float percentFilled = .8f;

	[SerializeField] private float maxTilesNumberWithHoles = 927;
	#region Explanation
	/*
	 * 1. Test # Of Tiles in Circle
	 * 2. Add Holes
	 */
	#endregion
	private float tilesAroundPlayer
	{
		get
		{
			float loadCircumference = Mathf.PI * loadRadius * 2;
			float tileArea = Mathf.Pow(tileDiameter / 2, 2);//Don't ask me why it's divided by 2. shhhhhhhhh

			float maxTilesInRadius = maxTilesNumberWithHoles - loadCircumference / tileArea;

			return maxTilesInRadius * percentFilled;
		}
	}

	#region Testing
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(Player.Instance.transform.position, loadRadius);
	}
	#endregion

	private void Start()
	{
		comparePlayerRoom = currentPlayerRoom;

		tg.SpawnFirstTile(firstTile);
	}

	private void Update()
	{
		if (TileManager.worldTiles.Count >= TileManager.Instance.tileLimit) return;

		#region Initial Spawn
		if (TileManager.worldTiles.Count < initialTiles)
		{
			if (t > 0)
				t -= Time.deltaTime;
			else
			{
				tg.GenerateTile(TileManager.worldTiles);
				t = timeBtwnSpawns;
			}
			return;
		}
		#endregion

		#region Generate				
		//2. Spawn Tile if Number of Tiles around Player aren't Met
		if (tilesInRange.Count <= tilesAroundPlayer)
		{
			if (t > 0)
				t -= Time.deltaTime;
			else
			{
				tg.GenerateTile(tilesInRange);
				t = timeBtwnSpawns;
			}
		}
		#endregion
	}
}
