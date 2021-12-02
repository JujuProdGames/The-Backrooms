using System;
using UnityEngine;

public class TriggerRingAction : BaseClass
{
	[Header("Spawn Audio")]
	[Range(10, 40)]
	[SerializeField] private float spawnRadius = 20f;
	[SerializeField] private GameObject appearance1, appearance2;

	[Header("Jumpscare")]
	[SerializeField] private GameObject monster;
	public static event EventHandler<OnJumpscareIdleArgs> onJumpscareIdle;
	public class OnJumpscareIdleArgs : EventArgs
	{
		public Vector3 monsterPos;
	}

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
				SpawnMonsterJumpscare(monster, Player.Instance.GetComponent<PlayerJumpscare>().jumpscareSpot, true);
				break;
				/*SpawnSound(appearance1);
				break;
			case 2:
				SpawnSound(appearance2);
				break;
			case 3:
				SpawnMonsterJumpscare(monster);
				break;*/
		}
	}

	private void SpawnSound(GameObject sound)
	{
		Vector3 randomPos = RandomVector3InCircle(Player.Instance.transform.position, spawnRadius);
		randomPos = new Vector3(randomPos.x, 0, randomPos.z);

		GameObject soundObject = Instantiate(sound, randomPos, Quaternion.identity);
		soundObject.GetComponent<AudioSource>().Play();
	}

	static GameObject jumpscare;
	public static Vector3 monsterPos
	{
		get
		{
			return jumpscare.GetComponent<Monster>().face.transform.position;
		}
	}
	private void SpawnMonsterJumpscare(GameObject monster, Transform jumpscareSpot, bool unparentObject = false)
	{
		jumpscare = Instantiate(monster, jumpscareSpot.position, jumpscareSpot.rotation, jumpscareSpot);
		
		if(unparentObject) jumpscare.transform.parent = null;		

		onJumpscareIdle(this, new OnJumpscareIdleArgs {monsterPos = jumpscare.transform.position});
	}
}
