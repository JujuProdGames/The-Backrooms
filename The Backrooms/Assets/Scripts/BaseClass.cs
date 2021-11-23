using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
	#region Methods

	#region Vector3
	public bool Vector3ExistsInList(Vector3 vector3, List<Vector3> list, bool roundVector3 = true)
	{
		if(roundVector3)
			vector3 = RoundToIntVector3(vector3);//I don't know why, I don't have to know why, but for some reason, when I round the vector3 before hand, it doesn't round it unless I put it here. Ok.

		foreach (Vector3 vector3InList in list)
		{
			if (vector3.x == vector3InList.x && vector3.y == vector3InList.y && vector3.z == vector3InList.z)
				return true;
		}

		return false;
	}

	public Vector3 RoundToIntVector3(Vector3 vectorToRound)
	{
		return new Vector3(
			Mathf.RoundToInt(vectorToRound.x),
			Mathf.RoundToInt(vectorToRound.y),
			Mathf.RoundToInt(vectorToRound.z)
			);
	}
	#endregion

	#endregion
}
