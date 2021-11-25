using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
	[SerializeField] private LayerMask tileMask;
	[Header("Tile Data")]
	[SerializeField] private float tileSize = 3f;

	[Header("Generate Tiles Around Player")]
	[Range(50, 300)]
	[SerializeField] private float loadRadius = 100;

	[Range(0, 1)]
	[SerializeField] private float percentFilled = .8f;

	[SerializeField] private float maxTilesNumberWithHoles = 223;
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
			float tileArea = Mathf.Pow(tileSize, 2);

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
		Debug.Log("Tiles Around Player: " + tilesAroundPlayer);
	}

	private void Update()
	{
		//1. Get list of all tiles around player
		List<Collider> tilesInRange = new List<Collider>(Physics.OverlapSphere(Player.Instance.transform.position, loadRadius, tileMask));
		Debug.Log("Tiles in Range: " + tilesInRange.Count);

		
	}
}
