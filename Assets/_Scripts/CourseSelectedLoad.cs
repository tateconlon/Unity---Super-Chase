using UnityEngine;
using System.Collections;

public class CourseSelectedLoad : MonoBehaviour {


	public void LoadSelectedCourse() {
		switch (GameControl.control.gameMode) {
		case 0:
			switch (GameControl.control.level) {
			case 0:
				Application.LoadLevel("Easy1P");
				break;
			case 1:
				Application.LoadLevel("Hard1P");
				break;
			}
			break;
		case 1:
			switch (GameControl.control.level) {
			case 0:
				Application.LoadLevel("EasyRace");
				break;
			case 1:
				Application.LoadLevel("HardRace");
				break;
			}
			break;
		case 2:
			switch (GameControl.control.level) {
			case 0:
				Application.LoadLevel("EasyChase");
				break;
			case 1:
				Application.LoadLevel("HardChase");
				break;
			}
			break;
		}
	}

}
