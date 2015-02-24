using UnityEngine;
using System.Collections;

public class InputAxisDownManager {

	bool pressedRight = false;
	bool pressedLeft = false;
	bool pressedUp = false;
	bool pressedDown = false;
	
	bool moveRight = false;
	bool moveLeft = false;
	bool moveUp = false;
	bool moveDown = false;
	
	// Update is called once per frame
	public void GetKeyDowns()
	{
		moveRight = false;
		moveLeft = false;
		moveUp = false;
		moveDown = false;

		if(Input.GetAxisRaw("HorizontalP1") > 0.01f && !pressedRight) {
			moveRight = true;
			pressedRight = true;
			pressedLeft = false;
		}
		else if(Input.GetAxisRaw ("HorizontalP1") < -0.01f && !pressedLeft) {
			moveLeft = true;
			pressedLeft = true;
			pressedRight = false;
		}
		else if(Input.GetAxisRaw ("HorizontalP1") == 0) {
			pressedLeft = false;
			pressedRight = false;
		}
		
		if(Input.GetAxisRaw("VerticalP1") > 0.01f && !pressedUp) {
			moveUp = true;
			pressedUp = true;
			pressedDown = false;
		}
		if(Input.GetAxisRaw("VerticalP1") < -0.01f && !pressedDown) {
			moveDown = true;
			pressedDown = true;
			pressedUp = false;
		}
		else if(Input.GetAxisRaw ("VerticalP1") == 0) {
			pressedUp = false;
			pressedDown = false;
		}
		
	}

	public bool isUpKeyDown(){
		return moveUp;
	}

	public bool isDownKeyDown(){
		return moveDown;
	}

	public bool isRightKeyDown(){
		return moveRight;
	}

	public bool isLeftKeyDown(){
		return moveLeft;
	}
}
