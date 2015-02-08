using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DemoScene : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[SerializeField]AudioClip jumpSound;
	[SerializeField]AudioClip landSound, stepSound, stepSound2;
	bool playStepOne = true;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	
	Character playerCharacter;
	[Header("Movement messages:")]
	[SerializeField]float timeSpentStopped = 0f;
	[SerializeField]List<InputAwareMessage> notWalkingMessages;
	[SerializeField]float timeSpentWalking = 0f;
	[SerializeField]List<InputAwareMessage> walkingMessages;
	
	[Header("Input messages:")]
	[SerializeField]List<CountedMessage> jumpMessages;
	int timesJumped = 0;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
		playerCharacter = GetComponent<Character>();
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
//		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
//		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

		if( Input.GetAxis("Move") > 0.01f )
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

            if (_controller.isGrounded)
                _animator.SetBool("moving", true);

			timeSpentWalking += Time.deltaTime;
			timeSpentStopped = 0f;
		}
		else if( Input.GetAxis("Move") < -0.01f )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.SetBool("moving", true);

			timeSpentWalking += Time.deltaTime;
			timeSpentStopped = 0f;
		}
		else
		{
            _animator.SetBool("moving", false);
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
            {
                if (Input.GetButtonDown("Punch"))
                    _animator.SetTrigger("punch");
			}

			timeSpentStopped += Time.deltaTime;
		}
		
		foreach(var tsw in walkingMessages){
			if(!tsw.used && timeSpentWalking > tsw.count){
				playerCharacter.Speak(tsw.message);
				tsw.used = true;
			}
		}
		foreach(var tss in notWalkingMessages){
			if(!tss.used && timeSpentStopped > tss.count){
				playerCharacter.Speak(tss.message);
				tss.used = true;
			}
		}

		// we can only jump whilst grounded
		if( _controller.isGrounded)
		{
            if(_animator.GetBool("jumping") == true){
				_animator.SetBool("jumping", false);
				audio.PlayOneShot(landSound);
			}
			
			if(Input.GetButtonDown("Jump"))
            {
			    _velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
                _animator.SetBool("jumping", true);
				timesJumped++;
				audio.PlayOneShot(jumpSound);
				foreach(var jm in jumpMessages){
					if(jm.count == timesJumped)playerCharacter.Speak(jm.message);
				}

            }
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move( _velocity * Time.deltaTime );
	}

	public void StepSound(){
		audio.PlayOneShot(((playStepOne)?stepSound:stepSound2));
		playStepOne = !playStepOne;
	}

}
[System.Serializable]
public class InputAwareMessage{
	public float count;
	public string message;
	public bool used = false;
	
	public InputAwareMessage(float count, string message){
		this.count = count;
		this.message = message;
		used = false;
	}
	
	
}