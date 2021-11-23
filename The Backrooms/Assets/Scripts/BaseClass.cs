using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
	private static readonly float EPSIILON = 0.01f;

	#region Methods

	#region Vector3
	public static bool Vector3Equals(Vector3 a, Vector3 b)
	{
		a = RoundToIntVector3(a);
		b = RoundToIntVector3(b);

		if (Mathf.Abs(a.x - b.x) < EPSIILON &&
			Mathf.Abs(a.y - b.y) < EPSIILON &&
			Mathf.Abs(a.z - b.z) < EPSIILON)
			return true;

		return false;
	}

	public static bool Vector3ExistsInList(Vector3 vector3, List<Vector3> list, bool roundVector3 = true)
	{
		if (roundVector3)
			vector3 = RoundToIntVector3(vector3);//I don't know why, I don't have to know why, but for some reason, when I round the vector3 before hand, it doesn't round it unless I put it here. Ok.

		foreach (Vector3 vector3InList in list)
		{
			if (Vector3Equals(vector3, vector3InList))
				return true;
		}

		return false;
	}

	public static Vector3 RoundToIntVector3(Vector3 vectorToRound)
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
