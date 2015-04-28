using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	public bool isMounted = false;// Whether or not the player is grounded.

	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%

	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;

	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	[SerializeField] LayerMask whatIsRope;				// LEONARDO - Adicionado na tentativa de fazer as cordas funcionarem.
														//DELETAR SE NAO FOR NECESSARIO

	[Range(0, 1)]

	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .1f;							// Radius of the overlap circle to determine if grounded

	bool grounded = false;								// Whether or not the player is grounded.
	bool onRope = false;			

	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.

	GameObject MovingPlatform;

	Vector3 _activeGlobalPlatformPoint;
	Vector3 _activeLocalPlatformPoint;
	public Vector3 PlatformVelocity {get; private set;}

    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();

	}


	void FixedUpdate()
	{

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetBool ("Mounted", isMounted);

		// Set the vertical animation
		anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}

	/// <summary>
	/// Move the character by the specified move, wheter he is walking crouching or jumping.
	/// </summary>
	/// <param name="move">Move the character by the specified move value.</param>
	/// <param name="crouch">If set to <c>true</c> player is crouching. Movement must adapt to it</param>
	/// <param name="jump">If set to <c>true</c> player is jumping. Movement must adapt to it</param>
	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if(!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		HandlePlatform();

		//only control the player if grounded or airControl is turned on
		if(grounded || airControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				facingRight = Flip.HorizontalFlip(transform, facingRight);
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				facingRight = Flip.HorizontalFlip(transform, facingRight);
		}

		//if the player is mounted
		if(isMounted == true){
			if (Input.GetKey(KeyCode.UpArrow))		//desmontar apertando up & jump
				//LEONARDO - DEVE HAVER UM AVISO NA TELA PARA QUE O JOGADOR SAIBA DISSO
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					isMounted = false;
					
					
				}
			}
		}

		//antes do pulo
		if(MovingPlatform != null){

			_activeGlobalPlatformPoint = transform.position;
			_activeLocalPlatformPoint = MovingPlatform.transform.InverseTransformPoint(transform.position);

			//Debug.DrawLine(transform);
		}

        // If the player should jump...
        if (grounded && jump) {
            // Add a vertical force to the player.
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

        }
	}
	
	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D coll){

		//checks whether or not the player must be set as mounted
		//if true
	    if (coll.gameObject.tag.ToLower() == "mount") {

			coll.gameObject.GetComponent<HingeJoint2D>().enabled = true;

			coll.gameObject.transform.SetParent(gameObject.transform, true);

			coll.gameObject.GetComponent<RideMount>().mounted = true;

			isMounted = true;

		}

		if (coll.gameObject.tag.ToLower() == "movingplatform") {

			if(MovingPlatform != coll.gameObject){
				MovingPlatform = coll.gameObject;

				_activeGlobalPlatformPoint = transform.position;
				_activeLocalPlatformPoint = MovingPlatform.transform.InverseTransformPoint(transform.position);
			}
		}
		
	}

	/// <summary>
	/// Raises the collision stay2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionStay2D(Collision2D coll) {

		//checks whether or not the player is "touching" a box
		//if true
		if(coll.gameObject.tag == "BOX"){

			//if the player is holding the left control key
			if (Input.GetKey (KeyCode.LeftControl)) {
				//sets the box mass to 8 so he can push it
				coll.gameObject.GetComponent<Rigidbody2D>().mass = 8;
			} 
			//else
			else {
				//sets the box mass to 1000 so it stays in place
				coll.gameObject.GetComponent<Rigidbody2D>().mass = 1000;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll){

		if (coll.gameObject.tag.ToLower() == "movingplatform") {
		
			MovingPlatform = null;
		}
	}

	void HandlePlatform(){

		if (MovingPlatform != null){

			var newGlobalPlatformPoint = MovingPlatform.transform.TransformPoint(_activeLocalPlatformPoint);
			var moveDistance = newGlobalPlatformPoint - _activeGlobalPlatformPoint;

			Debug.Log("Player position: " + transform.position);
			Debug.Log("Platform position: " + MovingPlatform.transform.position);
			Debug.Log("_activeLocalPlatformPoint position: " + _activeLocalPlatformPoint);
			Debug.Log(" --> newGlobalPlatformPoint position: " + newGlobalPlatformPoint);
			Debug.Log("_activeGlobalPlatformPoint position: " + _activeGlobalPlatformPoint);
			Debug.Log("Distance: " + moveDistance);
			if(moveDistance != Vector3.zero){
				transform.Translate(moveDistance, Space.World);
			}

			PlatformVelocity = moveDistance/Time.deltaTime;

		}else{
			PlatformVelocity = Vector3.zero;
		}

		//avoids keep using last update's position
		//MovingPlatform = null;
	}
}

