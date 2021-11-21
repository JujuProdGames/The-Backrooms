using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private List<Tile> tiles = new List<Tile>();

    void Start()
    {
        //Initial Spawn
        bool flipTile = (UnityEngine.Random.value > 0.5f);
        SpawnTile(tiles[UnityEngine.Random.Range(0, tiles.Count)], new Vector3(0, 0, 0), new Vector3(-90, UnityEngine.Random.Range(0, 4) * 90, 0), flipTile);
    }

    private void SpawnTile(Tile tileToBeSpawned, Vector3 position, Vector3 rotation, bool isFlipped)
	{
        float flipScaleMultiplier = isFlipped ? 1 : -1;

        GameObject newTile = Instantiate(tileToBeSpawned.tilePrefab, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
        newTile.transform.localScale = new Vector3(newTile.transform.localScale.x * flipScaleMultiplier, newTile.transform.localScale.y, newTile.transform.localScale.z);
	}
}
