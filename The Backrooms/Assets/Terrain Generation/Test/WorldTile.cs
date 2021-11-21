using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [SerializeField] private TileData tileData;

    // Start is called before the first frame update
    void Start()
    {
		CalculatePointPositions();		   
    }

	private void CalculatePointPositions()
	{
		for (int i = 0; i < 4; i++)
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

				case 0: pointPos = new Vector3(0, 0, tileSize); break;
				case 1: pointPos = new Vector3(tileSize, 0, 0); break;
				case 2: pointPos = new Vector3(0, 0, -tileSize); break;
				case 3: pointPos = new Vector3(-tileSize, 0, 0); break;
			}
			#endregion

			#region Spawn Points (If Eligible)
			switch (i)
			{
				case 0: if (tileData.connectTop) SpawnPoint(i.ToString(), pointPos); break;
				case 1: if (tileData.connectRight) SpawnPoint(i.ToString(), pointPos); break;
				case 2: if (tileData.connectBottom) SpawnPoint(i.ToString(), pointPos); break;
				case 3: if (tileData.connectLeft) SpawnPoint(i.ToString(), pointPos); break;
			}
			#endregion			
		}

		DistortTile();
	}

	private void SpawnPoint(string name, Vector3 position)
	{
		GameObject point = new GameObject(name);
		point.transform.parent = transform;
		point.transform.position = position;
	}

	private void DistortTile()
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Random.Range(0, 4) * 90, transform.localEulerAngles.z);		
	}
}
