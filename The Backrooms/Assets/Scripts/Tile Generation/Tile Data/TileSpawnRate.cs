using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Rate", menuName = "Tile/Tile Spawn Rate")]
public class TileSpawnRate : ScriptableObject
{
    [HideInInspector]
    public float SUM = 1f;

    public List<TileRate> tileRates = new List<TileRate>();
    public List<float> criticalValues {
        get
        {
            //1. Initalize List
			List<float> temp = new List<float>
			{
				0
			};
			float critValue = 0;

            //2. Add Critical Points to List
            foreach(TileRate tr in tileRates)
			{
                critValue += tr.spawnChance * 100;
                temp.Add(critValue);
			}

            //3. Return List
            return temp;
        }
    }

    [Space]

    [Range(0.05f, 0.2f)]
    public float distributionSize;

    public void GenerateRandomValues(float sum, float amountOfNumbers, float distribution)//Add float distribution
	{
		for (int i = 0; i < amountOfNumbers; i++)
		{
            tileRates[i].spawnChance = Mathf.Round(Random.Range(sum/(amountOfNumbers-i) - distribution, sum/ (amountOfNumbers - i) + distribution) * 100) / 100;
            sum -= tileRates[i].spawnChance;
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
