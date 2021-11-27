using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRingAction : BaseClass
{
	[Header("Spawn Audio")]
	[Range(10, 40)]
	[SerializeField] private float spawnRadius = 20f;
	[SerializeField] private GameObject appearance1, appearance2;

	#region Subscribe + Unsubscribe
	private void OnEnable()
	{
		TriggerRingCollision.onRingTriggered += TriggerEvent;
	}

	private void OnDisable()
	{
		TriggerRingCollision.onRingTriggered -= TriggerEvent;
	}
	#endregion

	private void TriggerEvent(object sender, TriggerRingCollision.TriggerRingData e)
	{
		Debug.Log("Player Collided " + e.collisionNumber + " times!");

		switch (e.collisionNumber)
		{
			case 1:
				SpawnSound(appearance1);
				break;
		}
	}

	private void SpawnSound(GameObject sound)
	{
		Vector3 randomPos = RandomVector3InCircle(Player.Instance.transform.position, spawnRadius);
		randomPos = new Vector3(randomPos.x, 0, randomPos.z);

		GameObject soundObject = Instantiate(sound, randomPos, Quaternion.identity);
		soundObject.GetComponent<AudioSource>().Play();
	}
}
