using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorEditor : Editor
{
	public override void OnInspectorGUI()//Override can make vars that ignore "default stuff"
	{		
		base.OnInspectorGUI();//Includes "default stuff"

		TileGenerator tg = (TileGenerator) target;

		if (GUILayout.Button("Generate First Tile"))
		{
			tg.SpawnFirstTile();
		}
	}
}