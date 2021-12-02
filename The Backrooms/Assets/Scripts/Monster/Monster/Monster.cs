using UnityEngine;

public class Monster : MonoBehaviour
{
	//I KNOW SINGLETON IS BAD BUT ... i'm just lazy sadge
	#region Singleton
	public static Monster Instance;
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}
	#endregion

	public Transform face;
}
