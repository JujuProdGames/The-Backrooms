using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : BaseClass
{
    [SerializeField] private Transform player;
	[SerializeField] private PlayerMovement pm;

    [Header("Look Values")]
    [Range(30, 200)]
    [SerializeField] private float sensitivity;
    private float xRotation = 0f;

	private float bobTimer;
    private float startYPos;
	[Header("Head Bobbing")]
	[Range(2, 15)]
	[SerializeField] private float bobbingSpeed = 14f;
	[Range(0, 0.64f)]
    [SerializeField] private float bobbingAmount = .05f;
	[Range(1.25f, 2.25f)]
	[SerializeField] private float bobRateMultiplier = 1.75f;
	private float bobSpeedCalculated
	{
		get
		{
			switch (pm.playerMovementState)
			{
				case PlayerMovement.PlayerMovementState.Walking: return bobbingSpeed;
				case PlayerMovement.PlayerMovementState.Running: return bobbingSpeed * bobRateMultiplier;
				case PlayerMovement.PlayerMovementState.Exhausted: return bobbingSpeed / bobRateMultiplier;
			}

			return bobbingSpeed;
		}
	}
	private float bobAmountCalculated
	{
		get
		{
			switch (pm.playerMovementState)
			{
				case PlayerMovement.PlayerMovementState.Walking: return bobbingAmount;
				case PlayerMovement.PlayerMovementState.Running: return bobbingAmount * bobRateMultiplier;
				case PlayerMovement.PlayerMovementState.Exhausted: return bobbingAmount / bobRateMultiplier;
			}

			return bobbingSpeed;
		}
	}

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;

		startYPos = transform.localPosition.y;
	}

	// Update is called once per frame
	void Update()
	{
		Look();

		if (pm.isMoving)
			BobHead();
	}

	private void Look()
	{
		float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		player.Rotate(Vector3.up * mouseX);
	}

	private void BobHead()
	{
		bobTimer += Time.deltaTime * bobSpeedCalculated;

		transform.localPosition = new Vector3(
			transform.localPosition.x,
			startYPos + Mathf.Sin(bobTimer) * bobAmountCalculated,
			transform.localPosition.z);
	}
}
