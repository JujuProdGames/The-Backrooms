using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : BaseClass
{
    public bool isMoving;
    public enum PlayerMovementState
	{
        Idle,
        Walking,
        Running,
		Exhausted
	}
    [HideInInspector] public PlayerMovementState playerMovementState
	{
		get
		{
			if (isMoving)
			{
				if (hasStamina)
					return Input.GetKey("left shift") ? PlayerMovementState.Running : PlayerMovementState.Walking;
				else
					return PlayerMovementState.Exhausted;
			}

            return PlayerMovementState.Idle;
        }
	}

    [Header("Components")]
    [SerializeField] private CharacterController controller;

	[HideInInspector] private Vector3 playerVelocity;
    [Header("Walk")]
    [Range(3,18)]
    [SerializeField] private float speed = 12f;

    [Header("Run")]
    [Range(1, 2)]
    [SerializeField] private float runMultiplier = 1.5f;

    [Header("Jumping")]
    [Range(1, 8)]
    [SerializeField] private float jumpHeight = 3f;
    [HideInInspector] public bool isGrounded
	{
		get
		{
            bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            return isGrounded;
        }
	}
    [SerializeField] private Transform groundCheck;
    [Range(0, .8f)]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Gravity")]
    [Range(-2, -50)]
    [SerializeField] private float gravity = -9.18f;

	[Header("Stamina")]
	[Range(0.25f, 3f)]
	[SerializeField] private float startStaminaAmount;
	private float staminaAmount;
	[Range(0.25f, 3f)]
	[SerializeField] private float startRechargeStaminaAmount = 1.5f;
	private float rechargeStaminaAmount;
	private bool hasStamina
	{
		get
		{
			return staminaAmount > 0 ? true : false;
		}
	}


	private void Start()
	{
		#region Stamina
		staminaAmount = startStaminaAmount;
		rechargeStaminaAmount = startRechargeStaminaAmount;
		#endregion
	}

	void Update()
	{
		StickToGround();

		Move();

		if (Input.GetButtonDown("Jump") && isGrounded) Jump();

		CalculateGravity();
	}

	private void CalculateGravity()
	{
		//Gravity
		playerVelocity.y += gravity * Time.deltaTime;

		controller.Move(playerVelocity * Time.deltaTime);
	}

	private void StickToGround()
	{
		//Stick to Ground
		if (isGrounded && playerVelocity.y < 0)
			playerVelocity.y = -2f;
	}

	private void Move()
	{
		CalculateStamina();

		#region Calculate Speed
		float tempMultiplier = runMultiplier;

		if (!hasStamina) tempMultiplier = 1 / runMultiplier;

		float tempSpeed = Input.GetKey("left shift") ? speed * tempMultiplier : speed;
		#endregion

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;
		move = Vector3.ClampMagnitude(move, 1);

		isMoving = Mathf.Abs(move.x) > 0 || Mathf.Abs(move.z) > 0 ? true : false;

		controller.Move(move * tempSpeed * Time.deltaTime);
	}

	private void CalculateStamina()
	{		
		if (hasStamina)
		{
			if (Input.GetKey("left shift"))
			{
				staminaAmount -= Time.deltaTime;
			}
			else
			{
				staminaAmount += Time.deltaTime;
			}
		}
		else
		{
			if(rechargeStaminaAmount > 0)
			{
				Debug.Log("Recharging Stamina...");
				rechargeStaminaAmount -= Time.deltaTime;
			}
			else
			{
				rechargeStaminaAmount = startRechargeStaminaAmount;
				staminaAmount = startStaminaAmount;
			}
		}

		staminaAmount = Mathf.Clamp(staminaAmount, -1, startStaminaAmount);
	}

	private void Jump()
	{
		playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
	}
}