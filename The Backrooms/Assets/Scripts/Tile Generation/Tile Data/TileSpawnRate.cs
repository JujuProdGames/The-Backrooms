using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Rate", menuName = "Tile/Tile Spawn Rate")]
public class TileSpawnRate : ScriptableObject
{
    public List<TileRate> tiles = new List<TileRate>();
}

[System.Serializable]
public class TileRate
{
    public TileData tile;

    [Range(0, .5f)]
    public float spawnChance;
}
