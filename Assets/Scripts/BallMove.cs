using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {

	public float horzForce = 5f;
	public float jumpForce = 1f;
	Vector2 horzForceVect;
	Vector2 jumpForceVect;

	InputAxisDownManager axisMan;

	// Use this for initialization
	void Start () {
		horzForceVect = new Vector2(horzForce,0);
		jumpForceVect = new Vector2(0, jumpForce);
		axisMan = new InputAxisDownManager();
	}

	void Update()
	{
		horzForceVect = new Vector2(horzForce, 0);
		jumpForceVect = new Vector2(0, jumpForce);
	}

	// Update is called once per frame
	void FixedUpdate () {
		axisMan.GetKeyDowns();
		if(axisMan.isRightKeyDown()){
			if(rigidbody2D.velocity.x >= -0.001f) {
				Debug.Log ("right");
				rigidbody2D.AddForce(horzForceVect, ForceMode2D.Impulse);
			}
			else {
				Debug.Log ("Right Stop");
				float velY = rigidbody2D.velocity.y;
				rigidbody2D.isKinematic = true;
				rigidbody2D.velocity = new Vector2(0, velY);
				rigidbody2D.isKinematic = false;
			}
		}

		if(axisMan.isLeftKeyDown()){
			if(rigidbody2D.velocity.x <= 0.001f) {
				Debug.Log ("left");
				rigidbody2D.AddForce(-horzForceVect, ForceMode2D.Impulse);
			}
			else {
				Debug.Log ("Left Stop");
				float velY = rigidbody2D.velocity.y;
				rigidbody2D.isKinematic = true;
				rigidbody2D.velocity = new Vector2(0, velY);
				rigidbody2D.isKinematic = false;
			}
		}

		if(axisMan.isUpKeyDown()) {
			Debug.Log ("Up");
			if(rigidbody2D.velocity.y < 0.001f)
				StopY ();

			rigidbody2D.AddForce(jumpForceVect - 0.5f*Physics2D.gravity, ForceMode2D.Impulse);
		}

		if(axisMan.isDownKeyDown()) {
			if(rigidbody2D.velocity.y > -0.001f) {
				Debug.Log ("Down");
				StopY ();
			}
			rigidbody2D.AddForce(-jumpForceVect, ForceMode2D.Impulse);
		}
	}

	void StopY() {
		float velX = rigidbody2D.velocity.x;
		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = new Vector2(velX, 0);
		rigidbody2D.isKinematic = false;
	}
}