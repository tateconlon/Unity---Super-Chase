using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float jumpFastAcceleration = 120f;
	public float jumpSlowAcceleration = 120f;
	public float wallFastJumpForce = 30f;
	public float wallSlowJumpForce = 15f;
	public float horzSlowAcceleration = 45f;
	public float horzFastAcceleration = 75f;
	public float wallCheckLength = 0.3f;
	public float drawLength = 1f;
	public float skinWidth = 0.2f;

	public bool isRunning;

	float currJumpAccel;
	float currHorzAccel;
	float currWallJumpForce;

	Bounds box;

	public bool grounded;
	public bool onCeiling;
	public bool onWallRight;
	public bool onWallLeft;

	void Start(){
		box = collider2D.bounds;
	}

	void FixedUpdate() {
		CheckWalls();

		//Set forces to fast if shift held
		if(Input.GetKey(KeyCode.LeftShift)) {	//TODO: Better Input Managing, take out of Fixed Update?
			currJumpAccel = jumpFastAcceleration;
			currHorzAccel = horzFastAcceleration;
			currWallJumpForce = wallFastJumpForce;
		}
		else {
			currJumpAccel = jumpSlowAcceleration;
			currHorzAccel = horzSlowAcceleration;
			currWallJumpForce = wallSlowJumpForce;
		}

		//Add forces according to Input
		rigidbody2D.AddForce(new Vector2(currHorzAccel*Time.deltaTime, 0f) * Input.GetAxisRaw("HorizontalP2"), ForceMode2D.Impulse);
		rigidbody2D.AddForce(new Vector2(0f, currJumpAccel*Time.deltaTime) * Input.GetAxisRaw("VerticalP2"), ForceMode2D.Impulse);

		//If on wall and Jump is pressed, spring from wall
		if(onCeiling && (Input.GetAxisRaw("VerticalP2") < 0 || Input.GetButton("Jump")))
			rigidbody2D.AddForce(new Vector2(0f, -currWallJumpForce), ForceMode2D.Impulse);
		if(grounded && (Input.GetAxisRaw("VerticalP2") > 0 || Input.GetButton("Jump")))
			rigidbody2D.AddForce(new Vector2(0f, currWallJumpForce), ForceMode2D.Impulse);
		if(onWallLeft && (Input.GetAxisRaw("HorizontalP2") > 0 || Input.GetButton("Jump")))
			rigidbody2D.AddForce(new Vector2(currWallJumpForce, 0f), ForceMode2D.Impulse);
		if(onWallRight && (Input.GetAxisRaw("HorizontalP2") < 0 || Input.GetButton("Jump")))
			rigidbody2D.AddForce(new Vector2(-currWallJumpForce, 0), ForceMode2D.Impulse);

		//Stick to ceiling
		if(onCeiling)
			rigidbody2D.gravityScale = -1;
		else
			rigidbody2D.gravityScale = 1;
	}

	/*Checks walls from WallCheck Length away TOWARDS character's collider.  If the collider found is tagged as Platform, then they are on platform
	Checks in 12 spots
	_| | |_
	_     _
	_     _
	 | | |
	 Skin width brings the corner edges in so that when for example the charcter is on the ground, it does not trigger as onWallRight and onWallLeft as well
	 (It lifts the bottom ones off the ground, top ones drop down etc.)
	 */
	void CheckWalls()
	{
		Vector3 rightInset = new Vector3(box.size.x/2f + wallCheckLength, 0f);
		Vector3 rightInsideInset = new Vector3(box.size.x/2f - skinWidth, 0f);
		Vector3 upInset = new Vector3(0f, box.size.y/2f + wallCheckLength, 0f);
		Vector3 upInsideInset = new Vector3(0f, box.size.y/2f + - skinWidth, 0f);


		RaycastHit2D hitInfoLeftTop = Physics2D.Raycast(transform.position - rightInset + upInsideInset, Camera.main.transform.right, wallCheckLength);
		RaycastHit2D hitInfoLeftMiddle = Physics2D.Raycast(transform.position - rightInset, Camera.main.transform.right, wallCheckLength);
		RaycastHit2D hitInfoLeftBottom = Physics2D.Raycast(transform.position - rightInset -upInsideInset, Camera.main.transform.right, wallCheckLength);


		RaycastHit2D hitInfoRightTop = Physics2D.Raycast(transform.position + rightInset + upInsideInset, -Camera.main.transform.right, wallCheckLength);
		RaycastHit2D hitInfoRightMiddle = Physics2D.Raycast(transform.position + rightInset, -Camera.main.transform.right, wallCheckLength);
		RaycastHit2D hitInfoRightBottom = Physics2D.Raycast(transform.position + rightInset - upInsideInset, -Camera.main.transform.right, wallCheckLength);

		RaycastHit2D hitInfoDownLeft = Physics2D.Raycast(transform.position - upInset - rightInsideInset, Camera.main.transform.up, wallCheckLength);
		RaycastHit2D hitInfoDownMiddle = Physics2D.Raycast(transform.position - upInset, Camera.main.transform.up, wallCheckLength);
		RaycastHit2D hitInfoDownRight = Physics2D.Raycast(transform.position - upInset + rightInsideInset, Camera.main.transform.up, wallCheckLength);

		RaycastHit2D hitInfoUpLeft = Physics2D.Raycast(transform.position + upInset - rightInsideInset, -Camera.main.transform.up, wallCheckLength);
		RaycastHit2D hitInfoUpMiddle = Physics2D.Raycast(transform.position + upInset, -Camera.main.transform.up, wallCheckLength);
		RaycastHit2D hitInfoUpRight = Physics2D.Raycast(transform.position + upInset + rightInsideInset, -Camera.main.transform.up, wallCheckLength);

		//Left Draw
		Debug.DrawLine(transform.position - rightInset + upInsideInset, transform.position - rightInset + upInsideInset - Camera.main.transform.right * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position - rightInset, transform.position - rightInset - Camera.main.transform.right * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position - rightInset - upInsideInset, transform.position - rightInset - upInsideInset - Camera.main.transform.right * wallCheckLength, Color.red);

		//Right Draw
		Debug.DrawLine(transform.position + rightInset + upInsideInset, transform.position + rightInset + upInsideInset + Camera.main.transform.right * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position + rightInset, transform.position + rightInset + Camera.main.transform.right * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position + rightInset - upInsideInset, transform.position + rightInset - upInsideInset + Camera.main.transform.right * wallCheckLength, Color.red);

		//Down Draw
		Debug.DrawLine(transform.position - upInset - rightInsideInset, transform.position - upInset  - rightInsideInset - Camera.main.transform.up * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position - upInset, transform.position - upInset - Camera.main.transform.up * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position - upInset + rightInsideInset, transform.position - upInset + rightInsideInset - Camera.main.transform.up * wallCheckLength, Color.red);

		//Up Draw
		Debug.DrawLine(transform.position + upInset - rightInsideInset, transform.position + upInset - rightInsideInset + Camera.main.transform.up * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position + upInset, transform.position + upInset + Camera.main.transform.up * wallCheckLength, Color.red);
		Debug.DrawLine(transform.position + upInset + rightInsideInset, transform.position + upInset + rightInsideInset + Camera.main.transform.up * wallCheckLength, Color.red);

			
		//Checking side hits
		if((hitInfoLeftTop.collider != null && hitInfoLeftTop.collider.tag == "Platform")
		   || (hitInfoLeftMiddle.collider != null && hitInfoLeftMiddle.collider.tag == "Platform") 
		   || (hitInfoLeftBottom.collider != null && hitInfoLeftBottom.collider.tag == "Platform")){
			onWallLeft = true;
		}
		else
			onWallLeft = false;

		if((hitInfoRightTop.collider != null && hitInfoRightTop.collider.tag == "Platform")
		   || (hitInfoRightMiddle.collider != null && hitInfoRightMiddle.collider.tag == "Platform")
		   || (hitInfoRightBottom.collider != null && hitInfoRightBottom.collider.tag == "Platform")) {
			onWallRight = true;
		}
		else
			onWallRight = false;

		if((hitInfoDownLeft.collider != null && hitInfoDownLeft.collider.tag == "Platform")
		   || (hitInfoDownMiddle.collider != null && hitInfoDownMiddle.collider.tag == "Platform")
		   || (hitInfoDownRight.collider != null && hitInfoDownRight.collider.tag == "Platform")) {
			grounded = true;
		}
		else
			grounded = false;

		if((hitInfoUpLeft.collider != null && hitInfoUpLeft.collider.tag == "Platform")
		   || (hitInfoUpMiddle.collider != null && hitInfoUpMiddle.collider.tag == "Platform")
		   || (hitInfoUpRight.collider != null && hitInfoUpRight.collider.tag == "Platform")) {
			onCeiling = true;
		}
		else {
			onCeiling = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

	}

}