using System;
using UnityEngine;

public class TriggerRingCollision : MonoBehaviour
{
	[Range(0, 8)]
	[SerializeField] private static float maxRings = 3;

	private bool hasCollided;
	[Header("Spawn New Ring")]
	[SerializeField] private GameObject ring;

	//Event Triggered
	[SerializeField] private static int collisionNumber = 0;	
	public static event EventHandler<TriggerRingData> onRingTriggered;
	public class TriggerRingData : EventArgs
	{
		public int collisionNumber = 0;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player") && !hasCollided && collisionNumber < maxRings)
		{			
			collisionNumber++;

			onRingTriggered?.Invoke(this, new TriggerRingData { collisionNumber = collisionNumber });

			SpawnRing(ring);

			Destroy(ring);
			Destroy(gameObject);

			hasCollided = true;
		}
	}

	private void SpawnRing(GameObject ring)
	{
		GameObject spawnedRing = Instantiate(ring, Player.Instance.transform.position, Quaternion.identity);
		spawnedRing.name = "Ring " + (collisionNumber + 1).ToString();
	}
}
