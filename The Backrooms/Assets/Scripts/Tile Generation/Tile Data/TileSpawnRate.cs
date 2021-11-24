using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Rate", menuName = "Tile/Tile Spawn Rate")]
public class TileSpawnRate : ScriptableObject
{
    public List<TileRate> tileRates = new List<TileRate>();
    public List<float> criticalValues {
        get
        {
			List<float> temp = new List<float>
			{
				0
			};
			float critValue = 0;

            foreach(TileRate tr in tileRates)
			{
                critValue += tr.spawnChance * 100;
                temp.Add(critValue);
			}

            return temp;
        }
    }
}

[System.Serializable]
public class TileRate
{
    public TileData tile;

    [Range(0, .5f)]
    public float spawnChance;
}
