using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float jumpFastAcceleration = 15f;
	public float jumpSlowAcceleration = 10f;
	public float wallJumpForce = 7.5f;
	//public float walkMaxSpeed = 25f;
	//public float runMaxSpeed = 35f;
	public float slowAcceleration = 15f;
	public float fastAcceleration = 20f;
	public float wallCheckLength = 0.02f;
	public float drawLength = 1f;
	public float skinWidth = 0.1f;
	//public float droppingFactor = 1.5f;

	public bool isJumping;
	public bool isRunning;
	public bool isDropping;

	float currMaxSpeed;
	float currAccel;

	public float velX;
	public float velY;

	Bounds box;

	public bool grounded;
	public bool onCeiling;
	//public bool newOnCieling;
	public bool onWallRight;
	public bool onWallLeft;
	public bool onAnyWall;

	void Start(){
		box = collider2D.bounds;
	}

	void Update() {
		CheckWalls ();
		if(Input.GetButton("Jump") || Input.GetAxisRaw("VerticalP2") > 0.01f)
			isJumping = true;
		else
			isJumping = false;

		if(Input.GetAxisRaw("VerticalP2") < -0.01f)
			isDropping = true;
		else
			isDropping = false;

		if(Input.GetKey (KeyCode.LeftShift))
			isRunning = true;
		else
			isRunning = false;
	}

	void FixedUpdate() {

		CheckWalls();
		if(onAnyWall) {
			velX = rigidbody2D.velocity.x;
			velY = rigidbody2D.velocity.y;
			//rigidbody2D.isKinematic = true;

			if(isRunning) {
				velX += fastAcceleration*Time.deltaTime * Input.GetAxisRaw("HorizontalP2");
				velY += jumpFastAcceleration*Time.deltaTime * Input.GetAxisRaw("VerticalP2");
			}
			else {
				velX += slowAcceleration*Time.deltaTime * Input.GetAxisRaw("HorizontalP2");
				velY += jumpSlowAcceleration*Time.deltaTime * Input.GetAxisRaw("VerticalP2");
			}

			if(grounded)
				velY = Mathf.Clamp (velY, 0, float.PositiveInfinity);
			if(onCeiling)
				velY = Mathf.Clamp (velY, float.NegativeInfinity, 0);
			if(onWallLeft)
				velX = Mathf.Clamp (velX, 0, float.PositiveInfinity);
			if(onWallRight)
				velX = Mathf.Clamp(velX, float.NegativeInfinity, 0);

			if(isJumping) {
				if(grounded)
					velY = wallJumpForce;
				else if(onCeiling)
					velY -= wallJumpForce;
				else  if(onWallLeft)
					velX += wallJumpForce;
				else if(onWallRight)
					velX -= wallJumpForce;
			}

			rigidbody2D.velocity = new Vector2(velX, velY);
		}
		else{
			rigidbody2D.isKinematic = false;
			if(isRunning) {
				rigidbody2D.AddForce(new Vector2(fastAcceleration*Time.deltaTime, 0f) * Input.GetAxisRaw("HorizontalP2"), ForceMode2D.Impulse);
				rigidbody2D.AddForce(new Vector2(0f, jumpFastAcceleration*Time.deltaTime) * Input.GetAxisRaw("VerticalP2"), ForceMode2D.Impulse);
			}
			else {
				rigidbody2D.AddForce(new Vector2(slowAcceleration*Time.deltaTime, 0f) * Input.GetAxisRaw("HorizontalP2"), ForceMode2D.Impulse);
				rigidbody2D.AddForce(new Vector2(0f, jumpSlowAcceleration*Time.deltaTime) * Input.GetAxisRaw("VerticalP2"), ForceMode2D.Impulse);
			}
			if(onCeiling)
				rigidbody2D.gravityScale = -1;
			else
				rigidbody2D.gravityScale = 1;
		}
	}

	/*void FixedUpdate() {
		if(isJumping) {
			if(grounded || velY < 0f) {
				velY = jumpSpeed*Time.deltaTime;
				grounded = false;
			}
			else
				velY += jumpSpeed*Time.deltaTime;
			if(onWallLeft) {
				velY = jumpSpeed;
				velX = wallJumpSpeed;
				onWallLeft = false;
			}
			else if(onWallRight){
				velY = jumpSpeed;
				velX = -wallJumpSpeed;
				onWallRight = false;
			}
		}


		if(isRunning) {
			currMaxSpeed = runMaxSpeed;
			currAccel = fastAcceleration;
		}
		else {
			currMaxSpeed = walkMaxSpeed;
			currAccel = slowAcceleration;
		}
		velX += currAccel*Time.deltaTime*Input.GetAxisRaw("HorizontalP2");
		velX = Mathf.Clamp(velX, -currMaxSpeed, currMaxSpeed);

		if(onWallLeft && !grounded && velY > 0)
			velX = 0;
		else if(onWallLeft)
			velX = Mathf.Clamp(velX, 0, currMaxSpeed);
		if(onWallRight && !grounded)
			velX = 0;
		else if(onWallRight)
			velX = Mathf.Clamp(velX, -currMaxSpeed, 0);

		if(grounded)
			velY = 0;
		else if(newOnCieling && !onCeiling){	//First frame on cieling
			velY = 0;
			onCeiling = true;
		}
		else if(isDropping)
			velY += Physics2D.gravity.y*Time.deltaTime*droppingFactor;
		else
			velY += Physics2D.gravity.y*Time.deltaTime;

		rigidbody2D.velocity = new Vector2(velX, velY);
		newOnCieling = false;	//Reset every frame
	}
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

		onAnyWall = grounded || onCeiling || onWallLeft || onWallRight;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.collider.tag == "Platform") {
			rigidbody2D.isKinematic = true;
			velX = rigidbody2D.velocity.x;
			velY = rigidbody2D.velocity.y;
			//onAnyWall = true;
			CheckWalls();	//Should set onAnyWall to True
			Debug.Log ("Wall Collision - OnAnyWall: " + onAnyWall);
		}

	}

	void OnCollisionStay2D(Collision2D coll) {
		if(coll.collider != null && coll.collider.tag == "Platform")
			rigidbody2D.isKinematic = true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if(coll.collider != null && coll.collider.tag == "Platform")
			rigidbody2D.isKinematic = false;
	}


}