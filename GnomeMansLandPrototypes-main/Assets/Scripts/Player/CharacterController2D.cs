using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	private Animator anim;
	public float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    [SerializeField] private LadderMovement m_LadderMovement;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	[HideInInspector] public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
    public float pushSpeed = 0.5f;
    [HideInInspector] public bool canFlip;
	[HideInInspector] public float moveSpeed;

	[HideInInspector] public bool canJump = true;
	[HideInInspector] public bool isJumping;
    [HideInInspector] public bool isPushing;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		canFlip = true;
	}


	private void FixedUpdate()
	{
		m_Grounded = false;
		canJump = true;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
            
            if (colliders[i].gameObject.CompareTag("Ladder") || colliders[i].gameObject.CompareTag("Trampoline"))
                {
                    //Debug.Log("Found ladder");
                    canJump = false;
                }
            if (colliders[i].gameObject.CompareTag("Movable"))
            {
                Debug.Log("Found movable obstacle");

            }
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		isJumping = jump;
		moveSpeed = move;

        if (isPushing)
        {
            Debug.Log("is pushing");
            Debug.Log("canFlip = " + canFlip);
            moveSpeed *= pushSpeed;
        }

		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			//// If crouching
			//if (crouch)
			//{
			//	// Reduce the speed by the crouchSpeed multiplier
			//	move *= m_CrouchSpeed;

			//	// Disable one of the colliders when crouching
			//	if (m_CrouchDisableCollider != null)
			//		m_CrouchDisableCollider.enabled = false;
			//} else
			//{
			//	// Enable the collider when not crouching
			//	if (m_CrouchDisableCollider != null)
			//		m_CrouchDisableCollider.enabled = true;
			//}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(moveSpeed * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (moveSpeed > 0 && !m_FacingRight)
			{
                if (canFlip)
				// ... flip the player.
				    Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (moveSpeed < 0 && m_FacingRight)
			{
                if (canFlip)
				// ... flip the player.
				    Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump && canJump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
        //Debug.Log("canFlip in Flip(): " + canFlip);
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if (hit.collider.CompareTag($"MovableObject"))
        // {
        //     if (hit.collider.gameObject.GetComponent<Rigidbody>() == null) return;
        //     var pushDir = new Vector3(hit.moveDirection.x, 0, 0);
        //     hit.collider.attachedRigidbody.velocity = pushDir * pushPower;
        // }
        // if (hit.collider.CompareTag("Trampoline"))
        // {
        //     Debug.Log("Hit trampoline");
        //     if (m_Grounded)
        //     {
        //         m_Grounded = false;
        //         m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        //     }
        // }
    }



    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Rigidbody body = hit.collider.attachedRigidbody;

    //     // no rigidbody
    //     if (body == null || body.isKinematic)
    //         return;

    //     // We dont want to push objects below us
    //     if (hit.moveDirection.y < -0.3f)
    //         return;

    //     // Calculate push direction from move direction,
    //     // we only push objects to the sides never up and down
    //     Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

    //     // If you know how fast your character is trying to move,
    //     // then you can also multiply the push velocity by that.

    //     // Apply the push
    //     body.velocity = pushDir * pushPower;
    // }
}
