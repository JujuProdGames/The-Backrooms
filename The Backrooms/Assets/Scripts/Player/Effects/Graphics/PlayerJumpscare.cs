using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using DitzeGames.Effects;

public class PlayerJumpscare : SceneClass
{
	[Header("Detecting Jumpscare")]
	[HideInInspector] public Transform jumpscareSpot;
	[SerializeField] private Camera playerCam;
	[SerializeField] private LayerMask monsterLayer;

	[Header("spoop")]
	[SerializeField] private float turnSpeed;
	[SerializeField] private Vector3 cameraShakeAmount = new Vector3(.25f, .25f, .25f);
	public event EventHandler onJumpscare;

	[Header("Transition")]
	[Range(0.5f, 3f)]
	[SerializeField] private float transitionTime = 1f;
	[SerializeField] private SceneCollection sceneCollection;
	#region Subscribe + Unsubscribe
	private void OnEnable()
	{
		TriggerRingAction.onJumpscareIdle += QueueJumpscare;
	}

	private void OnDisable()
	{
		TriggerRingAction.onJumpscareIdle -= QueueJumpscare;
	}
	#endregion

	private bool queueJumpscare = false;
	private void QueueJumpscare(object sender, TriggerRingAction.OnJumpscareIdleArgs e)
	{
		queueJumpscare = true;
	}

	private void Update()
	{
		//Check if we can jumpscare
		if (Monster.Instance == null) return;
		if (!queueJumpscare) return;
		if (!Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, Mathf.Infinity, monsterLayer)) return;

		//Jumpscare Player
		Jumpscare(TriggerRingAction.monsterPos);
	}

	private void Jumpscare(Vector3 monsterPos)
	{
		//Trigger Jumpscare Event
		onJumpscare(this, EventArgs.Empty);

		//Animate Camera - https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html
		Vector3 _direction = (monsterPos - transform.position).normalized;

		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

		Vector3 currentRotation = playerCam.transform.localEulerAngles;
		currentRotation.x = Mathf.SmoothStep(0, playerCam.transform.localEulerAngles.y, turnSpeed/8);
		playerCam.transform.localEulerAngles = currentRotation;

		//Effects
		CameraShake.ShakeOnce(1, 10, cameraShakeAmount, playerCam, true);

		//Transition
		if (transitionTime > 0)
			transitionTime -= Time.deltaTime;
		else
			Transition();
	}

	private bool switchScenes = false;
	private void Transition()
	{
		//Loads Scenes
		if(!switchScenes)
			LoadScenes(sceneCollection, sceneCollection.unloadScenes);

		switchScenes = true;
	}
}
