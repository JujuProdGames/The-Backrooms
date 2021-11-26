using UnityEngine;

public class TileSimulator : TileClass
{
	TileGenerator tg;

	[Header("tiles go BRRRRRRRRRRRR")]
	[Range(0, .25f)]
	[SerializeField] private float timeBtwnSpawns = .01f;
	float t;

	void Start()
    {
		tg = TileGenerator.Instance;
		tg.SpawnFirstTile();

		t = timeBtwnSpawns;
    }

	private void Update()
	{
		if (timeBtwnSpawns > 0)
		{
			if (t > 0)
				t -= Time.deltaTime;
			else
			{
				tg.GenerateTile(TileManager.worldTiles);
				t = timeBtwnSpawns;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
			tg.GenerateTile(TileManager.worldTiles);
	}
}
