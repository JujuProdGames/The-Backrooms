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
		transform.position = RoundToIntVector3(transform.position);

		CalculatePointPositions();		   
    }

	private void CalculatePointPositions()
	{		
		for (int i = 0; i < 4; i++)//Four Connection Points per Tile (sorry no scalability!)
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

		point.transform.position = RoundToIntVector3(point.transform.position);

		#region Check For Illegal Point
		//Destroy Point if Tile or Connection Point already exists in that Position
		if (Vector3ExistsInList(point.transform.position, TileManager.worldTilePositions) || Vector3ExistsInList(point.transform.position, TileManager.connectionPositions))
		{
			if(Vector3ExistsInList(point.transform.position, TileManager.connectionPositions))
				Debug.Log("connection point already exists there bruv");
			DestroyConnectionPoint(this, point.transform);
			return;
		}
		#endregion

		connectionPoints.Add(point.transform);
	}

	private void DistortTile()
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(0, 4) * 90, transform.localEulerAngles.z);		
	}

	#region Custom Methods
	private static void DestroyConnectionPoint(WorldTile connectTile, Transform connectPoint)
	{
		Destroy(connectPoint.gameObject);
		connectTile.connectionPoints.Remove(connectPoint);
	}

	private bool Vector3ExistsInList(Vector3 vector3, List<Vector3> list)
	{
		//I don't know why, I don't have to know why, but for some reason, when I round the vector3 it doesn't round it unless I put it here. Ok.
		vector3 = RoundToIntVector3(vector3);

		foreach (Vector3 vector3InList in list)
		{
			if (vector3.x == vector3InList.x && vector3.y == vector3InList.y && vector3.z == vector3InList.z)
				return true;
		}

		return false;
	}

	private Vector3 RoundToIntVector3(Vector3 vectorToRound)
	{
		return new Vector3(
			Mathf.RoundToInt(vectorToRound.x),
			Mathf.RoundToInt(vectorToRound.y),
			Mathf.RoundToInt(vectorToRound.z)
			);
	}
	#endregion
}
