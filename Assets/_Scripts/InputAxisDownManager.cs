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

	string horzAxis;
	string vertAxis;

	public InputAxisDownManager(int playerNum){
		if(playerNum == 1) {
			horzAxis = "HorizontalP1";
			vertAxis = "VerticalP1";
		} else if(playerNum == 2) {
			horzAxis = "HorizontalP2";
			vertAxis = "VerticalP2";
		}
	}
	
	// Update is called once per frame
	public void GetKeyDowns()
	{
		moveRight = false;
		moveLeft = false;
		moveUp = false;
		moveDown = false;

		if(Input.GetAxisRaw(horzAxis) > 0.01f && !pressedRight) {
			moveRight = true;
			pressedRight = true;
			pressedLeft = false;
		}
		else if(Input.GetAxisRaw (horzAxis) < -0.01f && !pressedLeft) {
			moveLeft = true;
			pressedLeft = true;
			pressedRight = false;
		}
		else if(Input.GetAxisRaw (horzAxis) == 0) {
			pressedLeft = false;
			pressedRight = false;
		}
		
		if(Input.GetAxisRaw(vertAxis) > 0.01f && !pressedUp) {
			moveUp = true;
			pressedUp = true;
			pressedDown = false;
		}
		if(Input.GetAxisRaw(vertAxis) < -0.01f && !pressedDown) {
			moveDown = true;
			pressedDown = true;
			pressedUp = false;
		}
		else if(Input.GetAxisRaw (vertAxis) == 0) {
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
