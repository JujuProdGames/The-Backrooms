using System;
using UnityEngine;

public class PlayerJumpscare : BaseClass
{
	[Header("Detecting Jumpscare")]
    [HideInInspector] public Transform jumpscareSpot;
	[SerializeField] private Camera playerCam;
	[SerializeField] private LayerMask monsterLayer;

	[Header("spoop")]
	[SerializeField] private float turnSpeed;
	public event EventHandler onJumpscare;
	#region Subscribe + Unsubscribe
	private void OnEnable()
	{
		TriggerRingAction.onJumpscareIdle += CheckForJumpscare;
	}										 
											 
	private void OnDisable()				 
	{										 
		TriggerRingAction.onJumpscareIdle -= CheckForJumpscare;
	}
	#endregion

	private bool checkForJumpscare = false;
	private Vector3 monsterPos;
	private void CheckForJumpscare(object sender, TriggerRingAction.OnJumpscareIdleArgs e)
	{
		checkForJumpscare = true;
		monsterPos = e.monsterPos;
	}

	private void Update()
	{		
		if (!checkForJumpscare) return;

		//Debug.Log(Mathf.Abs(startYCameraPos - Player.Instance.transform.localEulerAngles.y));
		//If Player Looks at Monster (close enough)... BOO!
		if (!Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, Mathf.Infinity, monsterLayer)) return;

		Jumpscare(monsterPos);
	}

	private void Jumpscare(Vector3 monsterPos)
	{
		Debug.Log("Jumpscare!");
		onJumpscare(this, EventArgs.Empty);

		//https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html

		//find the vector pointing from our position to the target
		Vector3 _direction = (monsterPos - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
	}
}
