using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] private TileData tileData;

	//[HideInInspector]
	public List<Transform> connectionPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
		#region Rounding
		transform.position = new Vector3(
			Mathf.RoundToInt(transform.position.x),
			Mathf.RoundToInt(transform.position.y),
			Mathf.RoundToInt(transform.position.z)
			);
		#endregion

		CalculatePointPositions();		   
    }

	private void CalculatePointPositions()
	{		
		for (int i = 0; i < 4; i++)//4 Connection Points per Tile (sorry no scalability!)
		{
			Vector3 pointPos = new Vector3(0, 0);

			#region Calculating Point Positions
			float tileSize = transform.localScale.x;

			switch (i)
			{
				#region Point Positions Diagram
				/* 
						  --- POINT POSITIONS ---				
								  (0,1)
						_________________________
						|						|
						|						|
						|						|
				(-1,0)	|		   Tile			|	(1,0)
						|						|
						|						|
						|						|
						|						|
						_________________________
								  (0,-1)
				 */
				#endregion

				case 0: pointPos = new Vector3(0, tileSize, 0); break;
				case 1: pointPos = new Vector3(tileSize, 0, 0); break;
				case 2: pointPos = new Vector3(0, -tileSize, 0); break;
				case 3: pointPos = new Vector3(-tileSize, 0, 0); break;
			}			
			#endregion

			#region Spawn Points (If Eligible)
			switch (i)
			{				
				case 0: if (tileData.connectTop) SpawnConnectionPoint(i.ToString(), pointPos); break;
				case 1: if (tileData.connectRight) SpawnConnectionPoint(i.ToString(), pointPos); break;
				case 2: if (tileData.connectBottom) SpawnConnectionPoint(i.ToString(), pointPos); break;
				case 3: if (tileData.connectLeft) SpawnConnectionPoint(i.ToString(), pointPos); break;
			}			
			#endregion			
		}

		//DistortTile();
	}

	private void SpawnConnectionPoint(string name, Vector3 position)
	{
		GameObject point = new GameObject(name);		
		point.transform.parent = transform;
		point.transform.localPosition = position;		

		#region Check For Illegal Point
		if (TileExistsInPosition(point.transform.position))
		{
			//Debug.Log("TILE DETECTED: " + point);
			Destroy(point);
			return;
		}
		#endregion

		connectionPoints.Add(point.transform);
	}

	private void DistortTile()
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(0, 4) * 90, transform.localEulerAngles.z);		
	}

	private bool TileExistsInPosition(Vector3 connectionPos)
	{
		#region Rounding
		connectionPos = new Vector3(
			Mathf.RoundToInt(connectionPos.x),
			Mathf.RoundToInt(connectionPos.y),
			Mathf.RoundToInt(connectionPos.z)
			);
		#endregion

		//Debug.Log("Connection Pos: " + connectionPos);
		//Debug.Log("# of Position Tiles: " + TileManager.worldTilePositions.Count);
		//Debug.Log("First Tile Pos: " + TileManager.worldTilePositions[0]);
		float tilePosX;
		float tilePosY;
		float tilePosZ;

		foreach (Vector3 tilePos in TileManager.worldTilePositions)
		{
			tilePosX = tilePos.x;
			tilePosY = tilePos.y;
			tilePosZ = tilePos.z;

			//Debug.Log("Connection Pos: " + connectionPos.x + " " + connectionPos.y + " " + connectionPos.z);
			//Debug.Log("Tile Pos: " + tilePosX + " " + tilePosY + " " + tilePosZ);

			if (connectionPos.x == tilePosX && connectionPos.y == tilePosY && connectionPos.z == tilePosZ)
				return true;
		}

		return false;
	}
}
