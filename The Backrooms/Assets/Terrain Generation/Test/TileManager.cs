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

    public static List<GameObject> worldTiles = new List<GameObject>();
}
