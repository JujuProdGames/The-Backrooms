using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerSound : MonoBehaviour
{
    AudioManager am;
    PlayerMovement pm;

    [Header("Footstep")]
    [Range(.1f, 1f)]
    [SerializeField] private float stepRate;
    private float stepCoolDown;
    [Range(1.25f, 2.25f)]
    [SerializeField] private float runRateMultiplier = 1.75f;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        pm = GetComponent<PlayerMovement>();

		#region Footsteps
		stepCoolDown = stepRate;
		#endregion
	}

	// Update is called once per frame
	void Update()
    {
        #region Footsteps
        stepCoolDown -= Time.deltaTime;
        stepCoolDown = Mathf.Clamp(stepCoolDown, -1, Mathf.Infinity);

        if(Input.GetKeyDown("left shift"))
		{
            stepCoolDown = -1;
		}

		if (pm.isGrounded && stepCoolDown < 0)
        {
            if (pm.isMoving && !am.IsPlaying("Player Footstep"))
            {
                am.Play("Player Footstep");

				switch (pm.playerMovementState)
				{
                    case PlayerMovement.PlayerMovementState.Walking:
                        stepCoolDown = stepRate;

                        break;
                    case PlayerMovement.PlayerMovementState.Running:
                        stepCoolDown = stepRate / runRateMultiplier;

                        break;
                    case PlayerMovement.PlayerMovementState.Exhausted:
                        stepCoolDown = stepRate * runRateMultiplier;
                        
                        break;
				}
            }         
        }
        #endregion

        #region Exhausted
        if (!pm.hasStamina && !am.IsPlaying("Player Exhausted")) am.PlayOneShot("Player Exhausted");
        else if(pm.hasStamina && am.IsPlaying("Player Exhausted")) am.Stop("Player Exhausted");
        #endregion
    }
}
