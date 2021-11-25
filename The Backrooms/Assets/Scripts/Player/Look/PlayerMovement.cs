using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;

    [Header("Walk")]
    [Range(3,18)]
    [SerializeField] private float speed = 12f;

    [Header("Run")]
    [Range(1, 2)]
    [SerializeField] private float speedMultiplier = 1.5f;

    [Header("Jumping")]
    [Range(1, 8)]
    [SerializeField] private float jumpHeight = 3f;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [Range(0, .8f)]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Gravity")]
    [Range(-2, -50)]
    [SerializeField] private float gravity = -9.18f;

    private Vector3 velocity;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		#region Stick to Ground
		if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
		#endregion

		#region Calculate Speed
		float tempSpeed = Input.GetKey("left shift") && isGrounded ? speed * speedMultiplier : speed;        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1);

        controller.Move(move * tempSpeed * Time.deltaTime);
		#endregion

		#region Calculate Jump
		if (Input.GetButtonDown("Jump") && isGrounded) Jump();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
		#endregion
	}

	private void Jump()
	{
		velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
	}
}