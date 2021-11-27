using System;
using UnityEngine;

public class TriggerRingCollision : MonoBehaviour
{
	[SerializeField] private GameObject parentObject;
	[SerializeField] private static int collisionNumber = 0;
	
	public static event EventHandler<TriggerRingData> onRingTriggered;
	public class TriggerRingData : EventArgs
	{
		public int collisionNumber = 0;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			collisionNumber++;
			onRingTriggered?.Invoke(this, new TriggerRingData { collisionNumber = collisionNumber });
			Destroy(parentObject);
		}
	}
}
