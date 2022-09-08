using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Declaring this class PlayerController
public class PlayerNavigationManager : MonoBehaviour
{

	//defining key characteristics for player navigation
	public bool isControllerActive = true;
	public float moveSpeed = 6.0F;
	public bool _FacingRight = true;
	public Transform playerObject;
	public float lastMove;
	Rigidbody rb;
	public Animator animator;
	public SpriteRenderer spriteRenderer;

	//private bool IsGrounded = true;
	void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody>();
	}

	void Update()

	{
		//checks if the player controler is not active
		if (!isControllerActive) return;


		//flips the character left and right depending on movement key pressed 
		float horiMove = moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		rb.AddForce(new Vector3(horiMove, 0, 0), ForceMode.VelocityChange);
		//animator.SetTrigger("Running");
		if(lastMove == 0)
        {
			//Debug.Log("idling");
			animator.SetTrigger("Idle");
		}
		if (lastMove < 0 || lastMove > 0 && lastMove != 0)
        {
			//Debug.Log("Running");
			animator.SetTrigger("Running");
		
			
		}

		if (horiMove != lastMove)
		{
			HoriMove(horiMove);
			

		}
		lastMove = horiMove;

	}

	//Left right movement of the player
	public void HoriMove(float horiMove)
	{
		if (horiMove > 0 && !_FacingRight)
		{
			spriteRenderer.flipX = false;
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (horiMove < 0 && _FacingRight)
		{
			spriteRenderer.flipX = true;
			Flip();
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_FacingRight = !_FacingRight;

		// Multiply the player's x local scale by -1.
		/*Vector3 theScale  = transform.localScale;
		theScale.x *= -1;
		transform.localScale = new Vector3 (theScale.x, transform.localScale.y, transform.localScale.z);*/
		if (!_FacingRight)
		{
			playerObject.rotation = new Quaternion(0, 180, 0, 0);
		}
		else if (_FacingRight)
		{
			playerObject.rotation = new Quaternion(0, 0, 0, 0);

		}

	}

}


