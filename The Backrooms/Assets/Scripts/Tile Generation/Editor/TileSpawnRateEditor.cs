using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSpawnRate))]
public class TileSPawnRateEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		TileSpawnRate tsr = (TileSpawnRate) target;

		
		if(GUILayout.Button("Generate Random Spawn Values"))
		{
			tsr.GenerateRandomValues(tsr.SUM, tsr.tileRates.Count, tsr.distributionSize);
		}
	}
}
