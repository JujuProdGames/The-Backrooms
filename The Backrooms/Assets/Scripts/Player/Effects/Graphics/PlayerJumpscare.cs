using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using DitzeGames.Effects;

public class PlayerJumpscare : BaseClass
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
	[SerializeField] private SceneCollection loadScenes, unloadScenes;
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

		//Debug.Log(Mathf.Abs(startYCameraPos - Player.Instance.transform.localEulerAngles.y));
		//If Player Looks at Monster (close enough)... BOO!
		if (!Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, Mathf.Infinity, monsterLayer)) return;

		Jumpscare(TriggerRingAction.monsterPos);
	}

	private void Jumpscare(Vector3 monsterPos)
	{
		Debug.Log("Jumpscare!");
		onJumpscare(this, EventArgs.Empty);

		#region Cam Anim
		//https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html

		//find the vector pointing from our position to the target
		Vector3 _direction = (monsterPos - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

		//rotate camera x rotation
		Vector3 currentRotation = playerCam.transform.localEulerAngles;
		currentRotation.x = Mathf.SmoothStep(0, playerCam.transform.localEulerAngles.y, turnSpeed/8);
		playerCam.transform.localEulerAngles = currentRotation;
		#endregion

		#region Effects
		CameraShake.ShakeOnce(1, 10, cameraShakeAmount, playerCam, true);
		#endregion
		if (transitionTime > 0)
			transitionTime -= Time.deltaTime;
		else
			Transition();
	}

	private void Transition()
	{
		foreach (SceneField scene in loadScenes.loadScenes)
		{
			SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
		}
		foreach (SceneField scene in unloadScenes.loadScenes)
		{
			SceneManager.UnloadSceneAsync(scene.SceneName);
		}
	}
}
